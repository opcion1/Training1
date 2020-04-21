using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Models.ViewModels;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;
using Training1.Infrastructure;

namespace Training1.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public AccountService(IAccountRepository accountRepository,
                                UserManager<AppUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                SignInManager<AppUser> signInManager,
                                IConfiguration configuration,
                                IHttpContextAccessor contextAccessor)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public Task EditAccount(string id, AppUser account, string currentRole, string formerRole)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountEditViewModel> GetEditAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            AccountEditViewModel vm = new AccountEditViewModel(_roleManager) { Account = user, Roles = GetItemsRole() };

            return vm;
        }

        public async Task<AccountListViewModel> GetModelForAccountList(string search, int? indexPage, string sortOrder)
        {
            int itemsPerPage = _configuration.GetValue<int>("ItemsPerPage");
            ICollection<AppUser> users = await _accountRepository.ListAsync();
            if (search != null)
            {
                users = users.Where(u => u.FullName.ToLower().Contains(search.ToLower()) || u.Email.ToLower().Contains(search.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "fullname_desc":
                    users = users.OrderByDescending(u => u.FullName).ToList();
                    break;
                case "email":
                    users = users.OrderBy(u => u.Email).ToList();
                    break;
                case "email_desc":
                    users = users.OrderByDescending(u => u.Email).ToList();
                    break;
                case "status":
                    users = users.OrderBy(u => u.AccountStatus.ToString()).ToList();
                    break;
                case "status_desc":
                    users = users.OrderByDescending(u => u.AccountStatus.ToString()).ToList();
                    break;
                default:
                    users = users.OrderBy(u => u.FullName).ToList();
                    break;
            }
            AccountListViewModel vm = new AccountListViewModel
            {
                Users = users
                    .Skip(((indexPage ?? 1) - 1) * itemsPerPage)
                    .Take(itemsPerPage),
                SearchStr = search,
                PageIndex = indexPage ?? 1,
                TotalItems = users.Count(),
                CurrentSort = sortOrder,
                FullNameSort = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "",
                EmailSort = sortOrder == "email" ? "email_desc" : "email",
                StatusSort = sortOrder == "status" ? "status_desc" : "status",
                ItemsPerPage = itemsPerPage
            };

            return vm;
        }

        public async Task LogOut()
        {
            _contextAccessor.HttpContext.Session.Set<string>("AppStyle", null);
            await _signInManager.SignOutAsync();
        }

        public async Task UpdateAccountStatus(string id, Status status)
        {
            await _accountRepository.UpdateAccountStatus(id, status);
        }

        private List<SelectListItem> GetItemsRole()
        {
            return _roleManager.Roles.Select(r => new SelectListItem { Text = r.Name }).ToList();
        }
    }
}
