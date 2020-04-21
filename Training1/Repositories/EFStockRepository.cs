
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public class EFStockRepository : EFRepositoryBase<Stock>, IStockRepository
    {
        public EFStockRepository(ProductContext context)
            : base(context)
        {
        }

        public override async Task<Stock> GetByIdAsync(int id)
        {
            var stock = await _dbSet
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            return stock;
        }

        public async Task<ICollection<Stock>> ListAsyncByProductId(int productId)
        {
            var stocks = _dbSet.Include(s => s.Product)
                            .Where(s => s.ProductId == productId);
            return await stocks.ToListAsync();
        }
    }
}
