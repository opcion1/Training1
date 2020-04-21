using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories.Interfaces
{
    public interface IMealFoodRepository
    {
        Task AddAsync(MealFood mealFood);

        Task DeleteByMealIdAsync(int mealId);

        Task DeleteAsync(int mealId, int foodId);
    }
}
