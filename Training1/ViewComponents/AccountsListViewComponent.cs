using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = await _accountRepository.ListAsync();
            return View(users);
        }
    }
}
