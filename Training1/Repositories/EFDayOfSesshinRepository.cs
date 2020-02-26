using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public class EFDayOfSesshinRepository : IDayOfSesshinRepository
    {
        private readonly ProductContext _productContext;
        public EFDayOfSesshinRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public async Task<ICollection<DayOfSesshin>> ListAsync(int sesshinId)
        {
            ICollection<DayOfSesshin> days = await _productContext.DaysOfSesshin
                .Where(d => d.SesshinId == sesshinId)
                .Include(d => d.Meals)
                .ThenInclude(md => md.MealFoods)
                .ThenInclude(f => f.Food).ToListAsync();

            return days;
        }
    }
}
