using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Repositories.Interfaces;

namespace Training1.Services.Interfaces
{
    public interface IMealService : IServiceBase<Meal>
    {
        IFoodRepository FoodRepository { get; }
        IMealFoodRepository MealFoodRepository { get; }
        string GetMealSesshinOwner(int mealId);

        Task CreateFoodAsync(Food food);

        Task EditFoodAsync(Food food);

        Task DeleteFoodAsync(int id);

        Task CreateMealFoodAsync(MealFood mealFood);

        Task DeleteMealFoodsByMealIdAsync(int mealId);

        Task DeleteMealFoodAsync(int mealId, int foodId);

        Task<bool> ExistsFood(int id);

        Task<MealFoodViewModel> GetMealFoodViewModel(int mealId, int sesshinId);

        Task<IEnumerable<Food>> SearchFoodByNameAsync(string searchText);
    }
}
