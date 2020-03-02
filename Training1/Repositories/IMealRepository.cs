using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public interface IMealRepository
    {
        IQueryable<Meal> Meals { get; }

        Task<ICollection<Meal>> ListAsync();
        Task<ICollection<Meal>> ListAsyncByDayOfSesshin(int sesshinIddayOfSesshinId);
        Task<Meal> GetByIdAsync(int id);
        Meal GetById(int id);

        Task AddAsync(Meal meal);
        Task AddMealFoodAsync(MealFood mealFood);

        Task UpdateAsync(Meal meal);

        Task DeleteAsync(int id);

        Task DeleteFoodMealAsync(int mealId);
        Task DeleteFoodMealAsync(int mealId, int foodId);

        bool MealExists(int id);

        string GetSesshinOwner(Meal meal);
    }
}
