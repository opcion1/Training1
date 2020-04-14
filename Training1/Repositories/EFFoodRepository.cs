using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Infrastructure;
using Training1.Models;

namespace Training1.Repositories
{
    public class EFFoodRepository : IFoodRepository
    {
        private readonly ProductContext _productContext;
        public EFFoodRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public IQueryable<Food> Foods => _productContext.Food;

        public async Task AddAsync(Food food)
        {
            _productContext.Add(food);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var food = await _productContext.Stock.FindAsync(id);
            _productContext.Stock.Remove(food);
            await _productContext.SaveChangesAsync();
        }

        public bool FoodExists(int id)
        {
            return Foods.Any(e => e.Id == id);
        }

        public async Task<Food> GetByIdAsync(int id)
        {
            return await Foods.FirstOrDefaultAsync(f => f.Id == id);
        }

        public Food GetByName(string name)
        {
            return Foods.FirstOrDefault(f => f.Name == name);
        }

        public async Task<Food> GetByNameAsync(string name)
        {
            return await Foods.FirstOrDefaultAsync(f => f.Name == name);
        }

        public async Task<List<Food>> SearchByNameAsync(string searchText)
        {
            return await Foods.Where(f => f.Name.ContainInsensitive(searchText)).ToListAsync();
        }

        public async Task UpdateAsync(Food food)
        {
            _productContext.Update(food);
            await _productContext.SaveChangesAsync();
        }
    }
}
