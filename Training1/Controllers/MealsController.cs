using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Authorization;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Repositories;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Controllers
{
    public class MealsController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IAuthorizationService _authorizationService;

        public MealsController(IMealService mealService,
                                    IAuthorizationService authorizationService)
        {
            _mealService = mealService;
            _authorizationService = authorizationService;
        }


        // GET: Meals/AddFood
        public async Task<IActionResult> AddFood(int mealId, int sesshinId)
        {
            MealFoodViewModel mealFoodView = await _mealService.GetMealFoodViewModel(mealId, sesshinId);
            return View(mealFoodView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFood(MealFoodViewModel mealFoodView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var foodExists = await _mealService.ExistsFood(mealFoodView.Food.Id);
                    if (!foodExists)
                    {
                        var canCreateFood = await _authorizationService.AuthorizeAsync(User, mealFoodView.Food, UserOperations.Create);
                        if (canCreateFood.Succeeded)
                        {
                            await _mealService.CreateFoodAsync(mealFoodView.Food);
                        }
                        else
                        {
                            return new ChallengeResult();
                        }
                    }

                    MealFood mealFood = new MealFood { FoodId = mealFoodView.Food.Id, MealId = mealFoodView.MealId };
                    var isAuthorized = await _authorizationService.AuthorizeAsync(User, mealFood, UserOperations.Create);
                    if (isAuthorized.Succeeded)
                    {
                        await _mealService.CreateMealFoodAsync(mealFood);
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
                return RedirectToAction("EditFood", "Meals", new { sesshinId = mealFoodView.SesshinId, mealId = mealFoodView.MealId, foodId = mealFoodView.Food.Id});
            }
            return View(mealFoodView);
        }


        // GET: Meals/EditFood
        public async Task<IActionResult> EditFood(int mealId, int foodId, int sesshinId)
        {
            Food food = await _mealService.GetFoodByIdAsync(foodId);
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
                    bool foodExists = await _mealService.ExistsFood(mealFoodView.Food.Id);
                    if (foodExists)
                    {
                        var canUpdateFood = await _authorizationService.AuthorizeAsync(User, mealFoodView.Food, UserOperations.Update);
                        if (canUpdateFood.Succeeded)
                        {
                            await _mealService.EditFoodAsync(mealFoodView.Food);
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
                await _mealService.DeleteMealFoodAsync(mealId, foodId);
                return RedirectToAction("Details", "Sesshins", new { id = sesshinId, mealId = mealId });
            }
            else
            {
                return new ChallengeResult();
            }
        }


        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> SearchFood(string searchText)
        {
            try
            {
                IEnumerable<Food> foods = await _mealService.SearchFoodByNameAsync(searchText);
                return Ok(foods);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
