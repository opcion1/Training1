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

        public async Task UpdateNumberOfPeopleAsync(int id, int numberOfPeople)
        {
            DayOfSesshin day = await _productContext.DaysOfSesshin.FirstOrDefaultAsync(d => d.DayOfSesshinId == id);
            if (day != null)
            {
                day.NumberOfPeople = numberOfPeople;
                List<DayOfSesshin> nextDays = await _productContext.DaysOfSesshin.Where(d => d.SesshinId == day.SesshinId && d.Date > day.Date).ToListAsync();
                foreach(DayOfSesshin nextDay in nextDays)
                {
                    nextDay.NumberOfPeople = numberOfPeople;
                }
                await _productContext.SaveChangesAsync();
            }
        }
    }
}
