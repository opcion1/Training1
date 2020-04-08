﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Training1.Authorization;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Repositories;

namespace Training1.Controllers
{
    public class MealsController : Controller
    {
        private readonly ISesshinRepository _sesshinRepository;
        private readonly IMealRepository _mealRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IAuthorizationService _authorizationService;

        public MealsController(ISesshinRepository sesshinRepository,
                                    IMealRepository mealRepository,
                                    IFoodRepository foodRepository,
                                    IAuthorizationService authorizationService)
        {
            _sesshinRepository = sesshinRepository;
            _mealRepository = mealRepository;
            _foodRepository = foodRepository;
            _authorizationService = authorizationService;
        }


        // GET: Meals/AddFood
        public async Task<IActionResult> AddFood(int mealId, int sesshinId)
        {
            Sesshin sesshin = await _sesshinRepository.GetByIdAsync(sesshinId);
            if (sesshin != null)
            {
                MealFoodViewModel mealFoodView = new MealFoodViewModel { MealId = mealId, SesshinId = sesshinId, Food = new Food { NumberOfPeople = sesshin.NumberOfPeople } };
                return View(mealFoodView);
            }
            else
                return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFood(MealFoodViewModel mealFoodView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!_foodRepository.FoodExists(mealFoodView.Food.FoodId))
                    {
                        var canCreateFood = await _authorizationService.AuthorizeAsync(User, mealFoodView.Food, UserOperations.Create);
                        if (canCreateFood.Succeeded)
                        {
                            await _foodRepository.AddAsync(mealFoodView.Food);
                        }
                        else
                        {
                            return new ChallengeResult();
                        }
                    }

                    MealFood mealFood = new MealFood { FoodId = mealFoodView.Food.FoodId, MealId = mealFoodView.MealId };
                    var isAuthorized = await _authorizationService.AuthorizeAsync(User, mealFood, UserOperations.Create);
                    if (isAuthorized.Succeeded)
                    {
                        await _mealRepository.AddMealFoodAsync(mealFood);
                    }
                    else
                    {
                        return new ChallengeResult();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("EditFood", "Meals", new { sesshinId = mealFoodView.SesshinId, mealId = mealFoodView.MealId, foodId = mealFoodView.Food.FoodId});
            }
            return View(mealFoodView);
        }


        // GET: Meals/EditFood
        public async Task<IActionResult> EditFood(int mealId, int foodId, int sesshinId)
        {
            Food food = await _foodRepository.GetByIdAsync(foodId);
            MealFoodViewModel mealFoodView = new MealFoodViewModel { MealId = mealId, SesshinId = sesshinId, Food = food };
            return View(mealFoodView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFood(MealFoodViewModel mealFoodView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_foodRepository.FoodExists(mealFoodView.Food.FoodId))
                    {
                        var canUpdateFood = await _authorizationService.AuthorizeAsync(User, mealFoodView.Food, UserOperations.Create);
                        if (canUpdateFood.Succeeded)
                        {
                            await _foodRepository.UpdateAsync(mealFoodView.Food);
                        }
                        else
                        {
                            return new ChallengeResult();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Details", "Sesshins", new { id = mealFoodView.SesshinId, mealId = mealFoodView.MealId });
            }
            return View(mealFoodView);
        }



        // POST: Sesshins/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMealFood(int mealId, int foodId, int sesshinId)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new MealFood { MealId = mealId, FoodId = foodId }, UserOperations.Delete);
            if (isAuthorized.Succeeded)
            {
                await _mealRepository.DeleteFoodMealAsync(mealId, foodId);
                return RedirectToAction("Details", "Sesshins", new { id = sesshinId, mealId = mealId });
            }
            else
            {
                return new ChallengeResult();
            }
        }
    }
}
