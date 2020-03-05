using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Training1.Areas.Identity.Data;
using Training1.Models;
using Training1.Repositories;

namespace Training1.Authorization
{
    public class ChefAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMealRepository _mealRepository; 

        public ChefAuthorizationHandler(UserManager<AppUser> userManager,
                                        IMealRepository mealRepository)
        {
            _userManager = userManager;
            _mealRepository = mealRepository;
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
            }

            return Task.CompletedTask;
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
            //Get the sesshin tenzo
            Meal meal = _mealRepository.GetById(mealFood.MealId);
            string mealFoodOwner = _mealRepository.GetSesshinOwner(meal);
            return (mealFoodOwner == _userManager.GetUserId(context.User));
        }

        private bool IsAuthorizedFoodOperation(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Food food)
        {
            //get 
            return true;
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
