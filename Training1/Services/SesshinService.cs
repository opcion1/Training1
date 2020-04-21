using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class SesshinService : ServiceBase<Sesshin>, ISesshinService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IDayOfSesshinRepository _dayOfSesshinRepository;

        public SesshinService(ISesshinRepository sesshinRepository,
                                IFoodRepository foodRepository,
                                IDayOfSesshinRepository dayOfSesshinRepository,
                                    IUnitOfWork unitOfWork)
            : base(sesshinRepository, unitOfWork)
        {
            _foodRepository = foodRepository;
            _dayOfSesshinRepository = dayOfSesshinRepository;
        }

        public async Task<IEnumerable<DayOfSesshin>> GetDaysOfSesshin(int sesshinId)
        {
            var days = await _dayOfSesshinRepository.ListAsyncBySesshinId(sesshinId);
            return days;
        }

        public async Task SetNumberOfPeopleByDayIdAsync(int id, int numberOfPeople)
        {
            await _dayOfSesshinRepository.UpdateNumberOfPeopleAsync(id, numberOfPeople);
            await _unitOfWork.CommitAsync();
        }

        public string GetSesshinOwner(int sesshinId)
        {
            var sesshin = GetById(sesshinId);
            return sesshin.AppUserId;
        }

        #region private methods
        private ICollection<DayOfSesshin> SetDaysOfTheSesshin(Sesshin sesshin)
        {
            List<DayOfSesshin> daysOfSesshins = new List<DayOfSesshin>();
            bool comingDay = true;
            for (DateTime loopDate = sesshin.StartDate.AddDays(-1); loopDate <= sesshin.EndDate; loopDate = loopDate.AddDays(1))
            {
                DayOfSesshin dayOfSesshin = new DayOfSesshin
                {
                    Date = loopDate,
                    NumberOfPeople = sesshin.NumberOfPeople,
                    Meals = SetDailyMeals(comingDay, loopDate == sesshin.EndDate)
                };
                daysOfSesshins.Add(dayOfSesshin);
                comingDay = false;
            }

            return daysOfSesshins;
        }

        private ICollection<Meal> SetDailyMeals(bool comingDay, bool lastDay)
        {
            List<Meal> meals = new List<Meal>();
            if (!comingDay)
            {
                meals.Add(AddGenMaiMeal());
                meals.Add(new Meal { Type = MealType.Lunch });
            }
            if (!lastDay)
                meals.Add(new Meal { Type = MealType.Diner });

            return meals;
        }

        private Meal AddGenMaiMeal()
        {
            Meal genMaiMeal = new Meal { Type = MealType.Genmai };
            Food genmai = _foodRepository.GetFirstOrDefaultByCondition(f => f.Name.ToLower() == "genmai");
            if (genmai == null)
            {
                genmai = new Food
                {
                    Name = "genmai",
                    Description = "Riz and vegetable soap",
                    Commentary = ""
                };
                _foodRepository.AddAsync(genmai);
            }
            if (genmai != null)
            {
                if (genMaiMeal.MealFoods == null)
                {
                    genMaiMeal.MealFoods = new List<MealFood>();
                }
                genMaiMeal.MealFoods.Add(new MealFood { Meal = genMaiMeal, Food = genmai });
            }

            return genMaiMeal;
        }
        #endregion
    }
}
