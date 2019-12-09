

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace Training1.Authorization
{
    public class AdminAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                            OperationAuthorizationRequirement requirement,
                                                            object resource)
        {
            // Administrators can do anything.
            if (IsUserAdmin(context))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private bool IsUserAdmin(AuthorizationHandlerContext context)
        {
            return (context.User?.IsInRole(Constants.UserAdministratorsRole) ?? false);
        }
    }
}
