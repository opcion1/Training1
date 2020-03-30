using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Models.ViewModels;
using Training1.Repositories;

namespace Training1.ViewComponents
{
    [Authorize(Roles = "Admin")]
    public class AccountsListViewComponent: ViewComponent
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<AppUser> _userManager;

        public AccountsListViewComponent(IAccountRepository accountRepository,
                                    UserManager<AppUser> userManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchStr, string sortOrder)
        {
            ICollection<AppUser> users = await _accountRepository.ListAsync();
            if (searchStr != null)
            {
                users = users.Where(u => u.FullName.ToLower().Contains(searchStr.ToLower()) || u.Email.ToLower().Contains(searchStr.ToLower())).ToList();
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
                Users = users,
                SearchStr = searchStr,
                CurrentSort = sortOrder,
                FullNameSort = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "",
                EmailSort = sortOrder == "email" ? "email_desc" : "email",
                StatusSort = sortOrder == "status" ? "status_desc" : "status"
            };
            return View(vm);
        }
    }
}
