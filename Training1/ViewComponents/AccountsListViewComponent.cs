using Microsoft.AspNetCore.Authorization;
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

        public AccountsListViewComponent(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchStr)
        {
            ICollection<AppUser> users = await _accountRepository.ListAsync();
            if (searchStr != null)
            {
                users = users.Where(u => u.FullName.ToLower().Contains(searchStr.ToLower()) || u.Email.ToLower().Contains(searchStr.ToLower())).ToList();
            }
            AccountListViewModel vm = new AccountListViewModel
            {
                Users = users,
                SearchStr = searchStr
            };
            return View(vm);
        }
    }
}
