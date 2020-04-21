using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public class EFIngredientRepository : EFRepositoryBase<Ingredient>, IIngredientRepository
    {
        public EFIngredientRepository(ProductContext productContext)
            : base(productContext)
        {
        }

        public override async Task<Ingredient> GetByIdAsync(int id)
        {
            return await _dbSet.Include(i => i.Product).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByFoodId(int foodId)
        {
            return await _dbSet.Include(i => i.Product).Where(e => e.FoodId == foodId).ToListAsync();
        }
    }
}
