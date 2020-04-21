using System.Threading.Tasks;
using Training1.Models;
using Training1.Models.ViewModels;

namespace Training1.Services.Interfaces
{
    public interface IIngredientService : IServiceBase<Ingredient>
    {
        Task<IngredientsViewModel> GetViewListIngredientsViewModelAsync(int foodId, int mealId);
    }
}
