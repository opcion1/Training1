using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Infrastructure;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class MealService : ServiceBase<Meal>, IMealService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMealFoodRepository _mealFoodRepository;
        private readonly IDayOfSesshinRepository _dayOfSesshinRepository;


        public IFoodRepository FoodRepository => _foodRepository;

        public IMealFoodRepository MealFoodRepository => _mealFoodRepository;

        public MealService(IMealRepository mealRepository,
                            IFoodRepository foodRepository,
                            IMealFoodRepository mealFoodRepository,
                            IDayOfSesshinRepository dayOfSesshinRepository,
                            IUnitOfWork unitOfWork)
            : base(mealRepository, unitOfWork)
        {
            _foodRepository = foodRepository;
            _mealFoodRepository = mealFoodRepository;
            _dayOfSesshinRepository = dayOfSesshinRepository;
        }

        public async Task CreateMealFoodAsync(MealFood mealFood)
        {
            await _mealFoodRepository.AddAsync(mealFood);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMealFoodAsync(int mealId, int foodId)
        {
            await _mealFoodRepository.DeleteAsync(mealId, foodId);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMealFoodsByMealIdAsync(int mealId)
        {
            await _mealFoodRepository.DeleteByMealIdAsync(mealId);
            await _unitOfWork.CommitAsync();
        }

        public string GetMealSesshinOwner(int mealId)
        {
            string mealOwnerId = ((IMealRepository)_entityRepository).GetMealSesshinOwner(mealId);
            return mealOwnerId;
        }


        public async Task<MealFoodViewModel> GetMealFoodViewModel(int mealId, int sesshinId)
        {
            //We want the number of people of the day of the meal
            Meal meal = await GetByIdAsync(mealId);
            DayOfSesshin day = await _dayOfSesshinRepository.GetByIdAsync(meal.DayOfSesshinId);

            MealFoodViewModel vm = new MealFoodViewModel { MealId = mealId, SesshinId = sesshinId, Food = new Food { NumberOfPeople = day.NumberOfPeople } };

            return vm;
        }

        #region Food methods
        public async Task CreateFoodAsync(Food food)
        {
            await _foodRepository.AddAsync(food);
            await _unitOfWork.CommitAsync();
        }
        public async Task EditFoodAsync(Food food)
        {
            _foodRepository.Update(food);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteFoodAsync(int id)
        {
            await _foodRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Food>> SearchFoodByNameAsync(string searchText)
        {
            return await _foodRepository.GetByConditionAsync(f => f.Name.ContainInsensitive(searchText));
        }

        public async Task<bool> ExistsFood(int id)
        {
            return await _foodRepository.Exists(id);
        }
        #endregion
    }
}
