

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public class EFSesshinRepository : ISesshinRepository
    {
        private readonly ProductContext _context;
        private readonly IFoodRepository _foodRepository;
        private readonly IDayOfSesshinRepository _dayOfSesshinRepository;
        public EFSesshinRepository(ProductContext context,
                                    IFoodRepository foodRepository,
                                    IDayOfSesshinRepository dayOfSesshinRepository)
        {
            _context = context;
            _foodRepository = foodRepository;
            _dayOfSesshinRepository = dayOfSesshinRepository;
        }

        public IQueryable<Sesshin> Sesshins => _context.Sesshin;

        public async Task<ICollection<Sesshin>> ListAsync()
        {
            ICollection<Sesshin> sesshins = await Sesshins.ToListAsync();

            foreach(Sesshin sesshin in sesshins)
            {

            }

            return sesshins;
        }

        public async Task<Sesshin> GetByIdAsync(int id)
        {
            return await Sesshins.FirstOrDefaultAsync(p => p.SesshinId == id);
        }

        public async Task AddAsync(Sesshin sesshin)
        {
            // Add Sesshin days and meals to the sesshin
            sesshin.Days = GetDaysOfTheSesshin(sesshin);
            _context.Add(sesshin);
            await _context.SaveChangesAsync();
        }

        private ICollection<DayOfSesshin> GetDaysOfTheSesshin(Sesshin sesshin)
        {
            List<DayOfSesshin> daysOfSesshins = new List<DayOfSesshin>();
            bool comingDay = true;
            for (DateTime loopDate = sesshin.StartDate.AddDays(-1); loopDate <= sesshin.EndDate;  loopDate = loopDate.AddDays(1))
            {
                DayOfSesshin dayOfSesshin = new DayOfSesshin
                {
                    Date = loopDate,
                    NumberOfPeople = sesshin.NumberOfPeople,
                    Meals = GetDailyMeals(comingDay)
                };
                daysOfSesshins.Add(dayOfSesshin);
                comingDay = false;
            }

            return daysOfSesshins;
        }

        private ICollection<Meal> GetDailyMeals(bool comingDay)
        {
            List<Meal> meals = new List<Meal>();
            if (!comingDay)
            {
                meals.Add(AddGenMaiMeal());
                meals.Add(new Meal { Type = MealType.Lunch });
            }
            meals.Add(new Meal { Type = MealType.Diner });

            return meals;
        }

        private Meal AddGenMaiMeal()
        {
            Meal genMaiMeal = new Meal { Type = MealType.Genmai };
            Food genMai = _foodRepository.GetByName("genMai");
            if (genMai == null)
            {
                genMai = new Food { 
                    Name = "genmai",
                    Description = "Riz and vegetable soap",
                    Commentary = ""
                };
                _foodRepository.AddAsync(genMai);
            }
            if (genMai != null)
            {
                genMaiMeal.MealFoods.Add(new MealFood { Meal = genMaiMeal, Food = genMai});
            }

            return genMaiMeal;
        }

        public async Task UpdateAsync(Sesshin sesshin)
        {
            _context.Update(sesshin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sesshin = await GetByIdAsync(id);
            _context.Sesshin.Remove(sesshin);
            await _context.SaveChangesAsync();
        }

        public bool SesshinExists(int id)
        {
            return Sesshins.Any(e => e.SesshinId == id);
        }
    }
}
