using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Training1.Areas.Identity.Data;
using Training1.Models;

namespace Training1.Authorization
{
    public class ChefAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {
        private readonly UserManager<AppUser> _userManager;

        public ChefAuthorizationHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                            OperationAuthorizationRequirement requirement,
                                                            object resource)
        {
            if (!IsUserChef(context))
            {
                return Task.CompletedTask;
            }

            switch (resource)
            {
                case Product product:
                case Stock stock:
                    if (IsCreateOrReadOrUpdateOperation(requirement))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case Sesshin sesshin:
                    if (IsAuthorizedSesshinOperation(context, requirement, resource as Sesshin)){
                        context.Succeed(requirement);
                    }
                    break;
            }

            return Task.CompletedTask;
        }

        private bool IsAuthorizedSesshinOperation(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Sesshin sesshin)
        {
            //Any chef can create sesshin
            if (requirement.Name == Constants.CreateOperationName || requirement.Name == Constants.ReadOperationName)
            {
                return true;
            }
            //Otherwise you should be the sesshin chef
            else if (sesshin.AppUserId == _userManager.GetUserId(context.User))
            {
                return true;
            }
            return false;
        }

        private bool IsUserChef(AuthorizationHandlerContext context)
        {
            return (context.User?.IsInRole(Constants.UserChefRole) ?? false);
        }

        private bool IsCreateOrReadOrUpdateOperation(OperationAuthorizationRequirement requirement)
        {
            return requirement.Name == Constants.CreateOperationName ||
                            requirement.Name == Constants.ReadOperationName ||
                            requirement.Name == Constants.UpdateOperationName;
        }
    }
}
