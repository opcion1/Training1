﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Models.ViewModels;

namespace Training1.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserValidator<AppUser> _userValidator;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        public AccountsController(IAuthorizationService authorizationService,
                                    RoleManager<IdentityRole> roleManager,
                                    UserManager<AppUser> userManager,
                                    SignInManager<AppUser> signInManager,
                                    IUserValidator<AppUser> userValidator,
                                    IPasswordHasher<AppUser> passwordHasher)
        {
            _authorizationService = authorizationService;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _userValidator = userValidator;
            _passwordHasher = passwordHasher;
        }


        [Authorize(Roles = "Admin")]
        public ViewResult Index(ActiveTab? tab, string searchStr)
        {
            AccountViewModel accountViewModel = new AccountViewModel { ActiveTab = tab ?? ActiveTab.Accounts, SearchStr = searchStr };

            return View(accountViewModel);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, user, UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                AccountEditViewModel vm = new AccountEditViewModel(_roleManager) { Account = user, CurrentRole = roles[0] };

                return View(vm);
            }
            else
            {
                return new ChallengeResult();
            }
        }


        // POST: Sesshins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, FullName, Email")]AppUser account, string currentRole, string formerRole)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = await _userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        var isAuthorized = await _authorizationService.AuthorizeAsync(User, user, UserOperations.Update);
                        if (isAuthorized.Succeeded)
                        {
                            user.UserName = account.Email;
                            user.Email = account.Email;
                            user.FullName = account.FullName;

                            IdentityResult validEmail = await _userValidator.ValidateAsync(_userManager, user);
                            if (!validEmail.Succeeded)
                            {
                                foreach (IdentityError error in validEmail.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                            }
                            else
                            {
                                var result = await _userManager.UpdateAsync(user);
                                if (result.Succeeded)
                                {
                                    if (currentRole != formerRole)
                                    {
                                        result = await _userManager.RemoveFromRoleAsync(user, formerRole);
                                        result = await _userManager.AddToRoleAsync(user, currentRole);
                                    }
                                    return await Edit(id);
                                }
                                else
                                {
                                    foreach (var error in result.Errors)
                                    {
                                        ModelState.AddModelError(string.Empty, error.Description);
                                    }
                                }
                            }
                        }
                        else
                        {
                            return new ChallengeResult();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Not Found");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return await Edit(id);
            }
            return await Edit(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(string id, Status status)
        {
            try
            {
                AppUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var isAuthorized = await _authorizationService.AuthorizeAsync(User, user, UserOperations.Update);
                    if (isAuthorized.Succeeded)
                    {
                        user.LockoutEnabled = (status == Status.Rejected);
                        if (user.LockoutEnabled)
                        {
                            user.LockoutEnd = DateTime.Now.AddYears(100);
                        }
                        user.AccountStatus = status;
                        await _userManager.UpdateAsync(user);
                    }
                    else
                    {
                        return new ChallengeResult();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Not Found");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction("Edit", new { id = id });
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
