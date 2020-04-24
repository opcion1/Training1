using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Models;

namespace Training1.Authorization
{
    public class AccountantAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                            OperationAuthorizationRequirement requirement,
                                                            object resource)
        {
            if (!IsUserAccoutant(context))
            {
                return Task.CompletedTask;
            }

            if (IsUserRejected(context))
            {
                return Task.CompletedTask;
            }

            if (IsUserSubmitted(context) && requirement.Name != Constants.ReadOperationName)
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
                case Food food:
                case MealFood mealFood:
                case DayOfSesshin day:
                    //Accountant can only read
                    if (requirement.Name == Constants.ReadOperationName)
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case AppUser user:
                    if (IsAuthorizedAdminOperation(context, requirement, resource as AppUser))
                    {
                        context.Succeed(requirement);
                    }
                    break;

            }

            return Task.CompletedTask;
        }

        private bool IsUserSubmitted(AuthorizationHandlerContext context)
        {
            string status = context.User.FindFirst("AccountStatus").Value;
            if (Enum.TryParse(status, out Status userStatus))
            {
                return userStatus == Status.Submitted;
            }
            return false;
        }

        private bool IsUserRejected(AuthorizationHandlerContext context)
        {
            string status = context.User.FindFirst("AccountStatus").Value;
            if (Enum.TryParse(status, out Status userStatus))
            {
                return userStatus == Status.Rejected;
            }
            return false;
        }

        private bool IsAuthorizedAdminOperation(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, AppUser appUser)
        {
            if (requirement.Name == Constants.ApproveOperationName || requirement.Name == Constants.RejectOperationName)
            {
                return false;
            }
            string userName = appUser.UserName;
            string currentUserName = context.User.Identity.Name;
            return (userName == currentUserName);
        }

        private bool IsUserAccoutant(AuthorizationHandlerContext context)
        {
            return (context.User?.IsInRole(Constants.UserAccountantRole) ?? false);
        }

        private bool IsCreateOrReadOrUpdateOperation(OperationAuthorizationRequirement requirement)
        {
            return requirement.Name == Constants.CreateOperationName ||
                            requirement.Name == Constants.ReadOperationName ||
                            requirement.Name == Constants.UpdateOperationName;
        }
    }
}
