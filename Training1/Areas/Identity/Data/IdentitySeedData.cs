using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Authorization;
using Training1.Models;

namespace Training1.Areas.Identity.Data
{
    public static class IdentitySeedData
    {
        public static void EnsurePopulated(
            AppIdentityContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager)
        {
            // Create default Users (if there are none)
            if (!dbContext.Users.Any())
            {
                CreateRoles(roleManager)
                    .GetAwaiter()
                    .GetResult();
                CreateUsers(userManager)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            //Create Roles (if they doesn't exist yet)
            if (!await roleManager.RoleExistsAsync(Constants.UserAdministratorsRole))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.UserAdministratorsRole));
            }
            if (!await roleManager.RoleExistsAsync(Constants.UserAccountantRole))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.UserAccountantRole));
            }
            if (!await roleManager.RoleExistsAsync(Constants.UserChefRole))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.UserChefRole));
            }
        }

        private static async Task CreateUsers(UserManager<AppUser> userManager)
        {
            //Create Users
            string password = "Wqa123edC!";
            AppUser adminUser = new AppUser { Email = "admin@myapp.com", UserName = "admin@myapp.com", FullName = "Administrator", AccountStatus =  Status.Approved};
            AppUser accountantUser = new AppUser { Email = "joe.accountant@myapp.com", UserName = "joe.accountant@myapp.com", FullName = "Joe Accountant", AccountStatus = Status.Approved };
            AppUser chefUser = new AppUser { Email = "jimmy.chef@myapp.com", UserName = "jimmy.chef@myapp.com", FullName = "Jimmy Chef", AccountStatus = Status.Approved };

            await userManager.CreateAsync(adminUser, password);
            await userManager.CreateAsync(accountantUser, password);
            await userManager.CreateAsync(chefUser, password);

            await userManager.AddToRoleAsync(adminUser, Constants.UserAdministratorsRole);
            await userManager.AddToRoleAsync(accountantUser, Constants.UserAccountantRole);
            await userManager.AddToRoleAsync(chefUser, Constants.UserChefRole);
        }
    }
}
