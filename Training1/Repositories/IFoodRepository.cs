using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public interface IFoodRepository
    {        
        IQueryable<Food> Foods { get; }
        Task<Food> GetByIdAsync(int id);
        Task<Food> GetByNameAsync(string name);
        Food GetByName(string name);
        Task<List<Food>> SearchByNameAsync(string searchText); 
        Task AddAsync(Food food);
        Task UpdateAsync(Food food);
        Task DeleteAsync(int id);
        bool FoodExists(int id);
    }
}
