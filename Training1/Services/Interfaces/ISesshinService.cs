using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Services.Interfaces
{
    public interface ISesshinService
    {
        ISesshinRepository Sesshin { get; }
        Task CreateAsync(Sesshin sesshin);
        Task EditAsync(Sesshin sesshin);
        Task DeleteAsync(int id);
    }
}
