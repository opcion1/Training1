using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models.ViewModels;
using Training1.Repositories;

namespace Training1.ViewComponents
{
    public class IngredientsListViewComponent : ViewComponent
    {
        private readonly IIngredientRepository _ingredientRepository;
        public IngredientsListViewComponent(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(
            int foodId, int mealId)
        {
            var ingredients = await _ingredientRepository.ListAsyncByFoodId(foodId);
            var ingredientsViewModel = new IngredientsViewModel { Ingredients = ingredients, FoodId = foodId, MealId = mealId };

            return View(ingredientsViewModel);
        }
    }
}
