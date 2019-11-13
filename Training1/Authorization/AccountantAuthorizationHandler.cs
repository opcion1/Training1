

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Training1.Areas.Identity.Data;

namespace Training1.Authorization
{
    public class AccountantAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                            OperationAuthorizationRequirement requirement)
        {
            if (context.User == null)
            {
                // Return Task.FromResult(0) if targeting a version of
                // .NET Framework older than 4.6:
                return Task.CompletedTask;
            }

            //Accountant can create/read or upadte anything
            if ((requirement.Name == Constants.CreateOperationName ||
                requirement.Name == Constants.ReadOperationName ||
                requirement.Name == Constants.UpdateOperationName) &&
                context.User.IsInRole(Constants.UserAccountantRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
