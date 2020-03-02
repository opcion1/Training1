using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public class EFIngredientRepository : IIngredientRepository
    {
        private readonly ProductContext _productContext;
        public EFIngredientRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public IQueryable<Ingredient> Ingredients => _productContext.Ingredient;

        public async Task AddAsync(Ingredient ingredient)
        {
            _productContext.Add(ingredient);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ingredient = await _productContext.Ingredient.FindAsync(id);
            _productContext.Ingredient.Remove(ingredient);
            await _productContext.SaveChangesAsync();
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            var ingredient = await _productContext.Ingredient.Include(i => i.Product).FirstOrDefaultAsync(i => i.IngredientId == id);
            return ingredient;
        }

        public bool IngredientExists(int id)
        {
            return Ingredients.Any(i => i.IngredientId == id);
        }

        public async Task<ICollection<Ingredient>> ListAsync()
        {
            return await Ingredients.ToListAsync();
        }

        public async Task<ICollection<Ingredient>> ListAsyncByFoodId(int foodId)
        {
            return await Ingredients.Where(i => i.FoodId == foodId).Include(i => i.Product).ToListAsync();
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            _productContext.Update(ingredient);
            await _productContext.SaveChangesAsync();
        }
    }
}
