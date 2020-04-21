using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public class EFMealFoodRepository : IMealFoodRepository
    {
        private readonly ProductContext _productContext;
        private DbSet<MealFood> _dbSet => _productContext.MealFood;
        public EFMealFoodRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public async Task AddAsync(MealFood mealFood)
        {
            await _dbSet.AddAsync(mealFood);
        }

        public async Task DeleteByMealIdAsync(int mealId)
        {
            var mealFoods = await _dbSet.Where(mf => mf.MealId == mealId).ToListAsync();
            _dbSet.RemoveRange(mealFoods);
        }

        public async Task DeleteAsync(int mealId, int foodId)
        {
            var mealFood = await _dbSet.FirstOrDefaultAsync(mf => mf.MealId == mealId && mf.FoodId == foodId);
            _dbSet.Remove(mealFood);
        }
    }
}
