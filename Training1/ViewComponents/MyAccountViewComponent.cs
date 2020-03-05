using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Authorization;

namespace Training1.ViewComponents
{
    public class MyAccountViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        public MyAccountViewComponent(
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(
            string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View();
        }
    }
}
