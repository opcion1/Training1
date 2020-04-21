using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models.ViewModels;
using Training1.Repositories;
using Training1.Services.Interfaces;

namespace Training1.ViewComponents
{
    public class IngredientsListViewComponent : ViewComponent
    {
        private readonly IIngredientService _ingredientService;
        public IngredientsListViewComponent(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        public async Task<IViewComponentResult> InvokeAsync(
            int foodId, int mealId)
        {
            IngredientsViewModel ingredientsViewModel = await _ingredientService.GetViewListIngredientsViewModelAsync(foodId, mealId);

            return View(ingredientsViewModel);
        }
    }
}
