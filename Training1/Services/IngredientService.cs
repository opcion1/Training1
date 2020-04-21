using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class IngredientService : ServiceBase<Ingredient>, IIngredientService
    {
        public IngredientService(IIngredientRepository ingredientRepository,
                                    IUnitOfWork unitOfWork)
            :base(ingredientRepository, unitOfWork)
        {
        }

        public async Task<IngredientsViewModel> GetViewListIngredientsViewModelAsync(int foodId, int mealId)
        {
            var ingredients = await ((IIngredientRepository)_entityRepository).GetIngredientsByFoodId(foodId);
            return new IngredientsViewModel { Ingredients = ingredients, FoodId = foodId, MealId = mealId };
        }
    }
}
