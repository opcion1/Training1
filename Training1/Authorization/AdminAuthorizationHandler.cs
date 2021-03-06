﻿

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;

namespace Training1.Authorization
{
    public class AdminAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                            OperationAuthorizationRequirement requirement,
                                                            object resource)
        {
            // Administrators can do anything.
            if (IsUserAdmin(context) && !IsUserRejected(context))
            {
                if (IsUserSubmitted(context) && requirement.Name != Constants.ReadOperationName)
                {
                    return Task.CompletedTask;
                }
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private bool IsUserAdmin(AuthorizationHandlerContext context)
        {
            return (context.User?.IsInRole(Constants.UserAdministratorsRole) ?? false);
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

        private bool IsUserSubmitted(AuthorizationHandlerContext context)
        {
            string status = context.User.FindFirst("AccountStatus").Value;
            if (Enum.TryParse(status, out Status userStatus))
            {
                return userStatus == Status.Submitted;
            }
            return false;
        }
    }
}
