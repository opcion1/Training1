using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;

namespace Training1.Infrastructure.Factory
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<AppUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity)
                .AddClaim(
                    new Claim("AccountStatus", user.AccountStatus.ToString()));
            ((ClaimsIdentity)principal.Identity)
                .AddClaim(
                    new Claim("AppStyle", user?.AppStyle ?? "flatly"));


            return principal;
        }
    }
}
