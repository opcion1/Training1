using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Training1.Models;

namespace Training1.Authorization
{
    public class ChefAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {

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
                case Product p:
                case Stock s:
                    if (IsCreateOrReadOrUpdateOperation(requirement))
                    {
                        context.Succeed(requirement);
                    }
                    break;
            }

            return Task.CompletedTask;
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
