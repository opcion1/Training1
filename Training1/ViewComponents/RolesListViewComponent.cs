using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Repositories;

namespace Training1.ViewComponents
{
    [Authorize(Roles = "Admin")]
    public class RolesListViewComponent : ViewComponent
    {
        private readonly IAccountRepository _accountRepository;

        public RolesListViewComponent(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = await _accountRepository.ListRolesAsync();
            return View(roles);
        }
    }
}
