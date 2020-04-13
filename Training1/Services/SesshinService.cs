using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class SesshinService : ISesshinService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISesshinRepository _sesshinRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IDayOfSesshinRepository _dayOfSesshinRepository;

        public ISesshinRepository Sesshin { get { return _sesshinRepository; } }

        public SesshinService(ISesshinRepository sesshinRepository,
                                IFoodRepository foodRepository,
                                IDayOfSesshinRepository dayOfSesshinRepository,
                                    IUnitOfWork unitOfWork)
        {
            _sesshinRepository = sesshinRepository;
            _foodRepository = foodRepository;
            _dayOfSesshinRepository = dayOfSesshinRepository;
            _unitOfWork = unitOfWork;
        }



        public async Task CreateAsync(Sesshin sesshin)
        {
            // Add Sesshin days and meals to the sesshin
            sesshin.Days = GetDaysOfTheSesshin(sesshin);
            await _sesshinRepository.AddAsync(sesshin);
            await _unitOfWork.CommitAsync();
        }

        public async Task EditAsync(Sesshin sesshin)
        {
            _sesshinRepository.Update(sesshin);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _sesshinRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        #region private methods
        private ICollection<DayOfSesshin> GetDaysOfTheSesshin(Sesshin sesshin)
        {
            List<DayOfSesshin> daysOfSesshins = new List<DayOfSesshin>();
            bool comingDay = true;
            for (DateTime loopDate = sesshin.StartDate.AddDays(-1); loopDate <= sesshin.EndDate; loopDate = loopDate.AddDays(1))
            {
                DayOfSesshin dayOfSesshin = new DayOfSesshin
                {
                    Date = loopDate,
                    NumberOfPeople = sesshin.NumberOfPeople,
                    Meals = GetDailyMeals(comingDay, loopDate == sesshin.EndDate)
                };
                daysOfSesshins.Add(dayOfSesshin);
                comingDay = false;
            }

            return daysOfSesshins;
        }

        private ICollection<Meal> GetDailyMeals(bool comingDay, bool lastDay)
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
            Food genmai = _foodRepository.GetByName("genmai");
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
