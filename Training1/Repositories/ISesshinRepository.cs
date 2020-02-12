

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public interface ISesshinRepository
    {
        IQueryable<Sesshin> Sesshins { get; }

        Task<List<Sesshin>> ListAsync();
        Task<Sesshin> GetByIdAsync(int id);

        Task AddAsync(Sesshin sesshin);

        Task UpdateAsync(Sesshin sesshin);

        Task DeleteAsync(int id);

        bool SesshinExists(int id);
    }
}
