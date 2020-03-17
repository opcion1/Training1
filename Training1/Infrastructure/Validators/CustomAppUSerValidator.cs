using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;

namespace Training1.Infrastructure.Validators
{
    public class CustomAppUserValidator : UserValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            
            IdentityResult result = await base.ValidateAsync(manager, user);

            List<IdentityError> errors = result.Succeeded ?
                new List<IdentityError>() : result.Errors.ToList();

            bool isUserEmailAllReadyUsed = await IsUserEmailAllReadyUsed(manager, user.Id, user.Email);
            if (isUserEmailAllReadyUsed)
            {
                errors.Add(new IdentityError { 
                    Code = "EmailAllReadyExists",
                    Description = "An account allready exists with this email"
                });
            }

            return errors.Count == 0 ? IdentityResult.Success
                : IdentityResult.Failed(errors.ToArray());
        }

        private async Task<bool> IsUserEmailAllReadyUsed(UserManager<AppUser> manager, string id, string email)
        {
            var user = await manager.FindByEmailAsync(email);
            return user != null && user.Id != id;
        }
    }
}
