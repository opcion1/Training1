using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Models.ViewModels;
using Training1.Services.Interfaces;

namespace Training1.ViewComponents
{
    [Authorize(Roles = "Admin")]
    public class AccountsListViewComponent: ViewComponent
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;

        public AccountsListViewComponent(IAccountService accountService,
                                    UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchStr, int? indexPage, string sortOrder)
        {
            AccountListViewModel vm = await _accountService.GetModelForAccountList(searchStr, indexPage, sortOrder);

            return View(vm);
        }
    }
}
