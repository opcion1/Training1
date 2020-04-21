using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public class EFMealRepository : EFRepositoryBase<Meal>, IMealRepository
    {
        public EFMealRepository(ProductContext productContext)
            :base(productContext)
        {
        }

        public string GetMealSesshinOwner(int mealId)
        {
            Sesshin sesshin = _context.Sesshin.FromSql("SESSHIN_FIND_BY_MEAL_ID @p0", mealId).FirstOrDefault();
            //DayOfSesshin day = _context.DaysOfSesshin.FirstOrDefault(d => d.Id == meal.DayOfSesshinId);
            //if (day != null)
            //{
            //    var sesshin = _context.Sesshin.FirstOrDefault(s => s.Id == day.SesshinId);
            //    if (sesshin != null)
            //    {
            //        return sesshin.AppUserId;
            //    }
            //}
            return sesshin.AppUserId;
        }

    }
}
