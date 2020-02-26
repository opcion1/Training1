using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public class EFMealRepository : IMealRepository
    {
        private readonly ProductContext _productContext;
        public EFMealRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public IQueryable<Meal> Meals => _productContext.Meal;

        public async Task AddAsync(Meal meal)
        {
            _productContext.Add(meal);
            await _productContext.SaveChangesAsync();
        }

        public async Task AddMealFoodAsync(MealFood mealFood)
        {
            _productContext.Add(mealFood);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var meal = await _productContext.Meal.FindAsync(id);
            _productContext.Meal.Remove(meal);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteFoodMealAsync(int mealId)
        {
            var mealFoods = await _productContext.MealFood.Where(mf => mf.MealId == mealId).ToListAsync();
            _productContext.MealFood.RemoveRange(mealFoods);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteFoodMealAsync(int mealId, int foodId)
        {
            var mealFood = await _productContext.MealFood.FirstOrDefaultAsync(mf => mf.MealId == mealId && mf.FoodId == foodId);
            _productContext.MealFood.Remove(mealFood);
            await _productContext.SaveChangesAsync();
        }

        public Meal GetById(int id)
        {
            return Meals.FirstOrDefault(m => m.MealId == id);
        }

        public async Task<Meal> GetByIdAsync(int id)
        {
            return await Meals.FirstOrDefaultAsync(m => m.MealId == id);
        }

        public string GetSesshinOwner(Meal meal)
        {
            DayOfSesshin day = _productContext.DaysOfSesshin.FirstOrDefault(d => d.DayOfSesshinId == meal.DayOfSesshinId);
            if (day != null)
            {
                var sesshin = _productContext.Sesshin.FirstOrDefault(s => s.SesshinId == day.SesshinId);
                if (sesshin != null)
                {
                    return sesshin.AppUserId;
                }
            }
            return String.Empty;
        }

        public async Task<ICollection<Meal>> ListAsync()
        {
            return await Meals.ToListAsync();
        }

        public async Task<ICollection<Meal>> ListAsyncByDayOfSesshin(int dayOfSesshinId)
        {
            return await Meals.Where(m => m.DayOfSesshinId == dayOfSesshinId).ToListAsync();
        }

        public bool MealExists(int id)
        {
            return Meals.Any(m => m.MealId == id);
        }

        public async Task UpdateAsync(Meal meal)
        {
            _productContext.Update(meal);
            await _productContext.SaveChangesAsync();
        }
    }
}
