

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public interface IStockRepository
    {
        IQueryable<Stock> Stocks { get; }

        Task<ICollection<Stock>> ListAsync();
        Task<ICollection<Stock>> ListAsyncByProductId(int productId);
        Task<Stock> GetByIdAsync(int id);

        Task AddAsync(Stock stock);

        Task UpdateAsync(Stock stock);

        Task DeleteAsync(int id);

        bool StockExists(int id);
    }
}
