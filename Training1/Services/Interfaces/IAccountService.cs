using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Models.ViewModels;

namespace Training1.Services.Interfaces
{
    public interface IAccountService
    {
        Task UpdateAccountStatus(string id, Status status);
        Task<AccountListViewModel> GetModelForAccountList(string search, int? indexPage, string sortOrder);
        Task<AccountEditViewModel> GetEditAccount(string id);
        Task EditAccount(string id, AppUser account, string currentRole, string formerRole);
        Task LogOut();
    }
}
