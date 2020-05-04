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

            return sesshin.AppUserId;
        }

    }
}
