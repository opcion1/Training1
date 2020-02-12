using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;
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
                    //Accountant can only read
                    if (requirement.Name == Constants.ReadOperationName)
                    {
                        context.Succeed(requirement);
                    }
                    break;
            }

            return Task.CompletedTask;
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
