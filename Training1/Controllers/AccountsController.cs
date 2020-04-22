using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Infrastructure;
using Training1.Models.ViewModels;
using Training1.Services.Interfaces;

namespace Training1.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserValidator<AppUser> _userValidator;
        public AccountsController(IAuthorizationService authorizationService,
                                    UserManager<AppUser> userManager,
                                    IUserValidator<AppUser> userValidator,
                                    IAccountService accountService)
        {
            _authorizationService = authorizationService;
            _accountService = accountService;
            _userManager = userManager;
            _userValidator = userValidator;
        }


        [Authorize(Roles = "Admin")]
        public async Task<ViewResult> Index(string searchStr, int? indexPage, string sortOrder)
        {
            AccountListViewModel vm = await _accountService.GetModelForAccountList(searchStr, indexPage, sortOrder);
            return View(vm);
        }


        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new AppUser { Id = id }, UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                AccountEditViewModel vm = await _accountService.GetEditAccount(id);

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
                                    return RedirectToAction(nameof(Edit), new { id = id });
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
                return View(_accountService.GetEditAccount(id));
            }
            return View(_accountService.GetEditAccount(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(string id, Status status)
        {
            try
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(User, new AppUser { Id = id }, UserOperations.Update);
                if (isAuthorized.Succeeded)
                {
                    await _accountService.UpdateAccountStatus(id, status);
                }
                else
                {
                    return new ChallengeResult();
                }
            }
            catch (KeyNotFoundException)
            {
                ModelState.AddModelError("", "User Not Found");
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction("Edit", new { id = id });
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
