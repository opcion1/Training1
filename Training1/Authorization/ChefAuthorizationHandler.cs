using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Training1.Areas.Identity.Data;
using Training1.Models;
using Training1.Repositories;
using Training1.Services.Interfaces;

namespace Training1.Authorization
{
    public class ChefAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMealService _mealService;
        private readonly ISesshinService _sesshinService;

        public ChefAuthorizationHandler(UserManager<AppUser> userManager,
                                        IMealService mealService,
                                        ISesshinService sesshinService)
        {
            _userManager = userManager;
            _mealService = mealService;
            _sesshinService = sesshinService;

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                            OperationAuthorizationRequirement requirement,
                                                            object resource)
        {
            if (!IsUserChef(context))
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
                    if (IsAuthorizedSesshinOperation(context, requirement, resource as Sesshin)){
                        context.Succeed(requirement);
                    }
                    break;
                case Food food:
                    if (IsAuthorizedFoodOperation(context, requirement, resource as Food))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                case MealFood mealFood:
                    if (IsAuthorizedMealFoodOperation(context, requirement, resource as MealFood))
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
                case DayOfSesshin day:
                    if (IsAuthorizedDayOperation(context, requirement, resource as DayOfSesshin))
                    {
                        context.Succeed(requirement);
                    }
                    break;
            }

            return Task.CompletedTask;
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

        private bool IsAuthorizedAdminOperation(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, AppUser appUser)
        {
            if (requirement.Name == Constants.ApproveOperationName || requirement.Name == Constants.RejectOperationName)
            {
                return false;
            }
            string userId = appUser.Id;
            string currentUserID = _userManager.GetUserId(context.User);
            return (userId == currentUserID);
        }

        private bool IsAuthorizedMealFoodOperation(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, MealFood mealFood)
        {
            if (requirement.Name == Constants.ReadOperationName)
            {
                return true;
            }
            //Get the sesshin tenzo
            string mealFoodOwner = _mealService.GetMealSesshinOwner(mealFood.MealId);
            return (mealFoodOwner == _userManager.GetUserId(context.User));
        }

        private bool IsAuthorizedDayOperation(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, DayOfSesshin dayOfSesshin)
        {
            //Get the sesshin tenzo
            if (requirement.Name == Constants.ReadOperationName)
            {
                return true;
            }
            string sesshinOwner = _sesshinService.GetSesshinOwner(dayOfSesshin.SesshinId);
            return (sesshinOwner == _userManager.GetUserId(context.User));
        }

        private bool IsAuthorizedFoodOperation(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Food food)
        {
            //get 
            return requirement.Name != Constants.DeleteOperationName;
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
