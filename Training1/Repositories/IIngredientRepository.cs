using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public interface IIngredientRepository
    {
        IQueryable<Ingredient> Ingredients { get; }

        Task<ICollection<Ingredient>> ListAsync();
        Task<ICollection<Ingredient>> ListAsyncByFoodId(int foodId);
        Task<Ingredient> GetByIdAsync(int id);

        Task AddAsync(Ingredient ingredient);

        Task UpdateAsync(Ingredient ingredient);

        Task DeleteAsync(int id);

        bool IngredientExists(int id);
    }
}
