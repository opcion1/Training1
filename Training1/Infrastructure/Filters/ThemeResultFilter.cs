using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;

namespace Training1.Infrastructure.Filters
{
    public class ThemeResultFilter : Attribute, IAsyncResultFilter
    {
        private readonly UserManager<AppUser> _userManager;

        public ThemeResultFilter(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Controller is Controller)
            {
                string appStyle = context.HttpContext.Session.Get<string>("AppStyle");
                if (String.IsNullOrEmpty(appStyle))
                {
                    string userId = _userManager.GetUserId(context.HttpContext.User);
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    appStyle = user.AppStyle ?? "flatly";
                    context.HttpContext.Session.Set<string>("AppStyle", appStyle);
                }   
                ((Controller)context.Controller).ViewData["appStyle"] = appStyle;
            }

            await next.Invoke();
        }
    }
}
