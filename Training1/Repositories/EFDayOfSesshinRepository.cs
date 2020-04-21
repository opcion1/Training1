using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public class EFDayOfSesshinRepository : EFRepositoryBase<DayOfSesshin>, IDayOfSesshinRepository
    {
        public EFDayOfSesshinRepository(ProductContext productContext)
            : base(productContext)
        {
        }
        public async Task<IEnumerable<DayOfSesshin>> ListAsyncBySesshinId(int sesshinId)
        {
            ICollection<DayOfSesshin> days = await _dbSet
                .Where(d => d.SesshinId == sesshinId)
                .Include(d => d.Meals)
                .ThenInclude(md => md.MealFoods)
                .ThenInclude(f => f.Food).ToListAsync();

            return days;
        }

        public async Task UpdateNumberOfPeopleAsync(int id, int numberOfPeople)
        {
            DayOfSesshin day = await _dbSet.FirstOrDefaultAsync(d => d.Id == id);
            if (day != null)
            {
                day.NumberOfPeople = numberOfPeople;
                List<DayOfSesshin> nextDays = await _dbSet.Where(d => d.SesshinId == day.SesshinId && d.Date > day.Date).ToListAsync();
                foreach(DayOfSesshin nextDay in nextDays)
                {
                    nextDay.NumberOfPeople = numberOfPeople;
                }
            }
        }
    }
}
