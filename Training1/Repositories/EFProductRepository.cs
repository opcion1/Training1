

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public class EFProductRepository : EFRepositoryBase<Product>, IProductRepository
    {
        public EFProductRepository(ProductContext context) 
            : base(context)
        {
        }

        public async Task<Product> GetByIdWithStocksAsync(int id)
        {
            return await _dbSet
                .Where(p => p.Id == id)
                .Include(p => p.Stocks)
                .FirstOrDefaultAsync();
        }
    }
}
