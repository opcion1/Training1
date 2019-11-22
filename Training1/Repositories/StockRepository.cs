
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ProductContext _productContext;
        public StockRepository(ProductContext context)
        {
            _productContext = context;
        }
        public IQueryable<Stock> Stocks => _productContext.Stock.Include(s => s.Product);

        public async Task AddAsync(Stock stock)
        {
            _productContext.Add(stock);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stock = await _productContext.Stock.FindAsync(id);
            _productContext.Stock.Remove(stock);
            await _productContext.SaveChangesAsync();
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            var stock = await _productContext.Stock
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.StockId == id);
            return stock;
        }

        public async Task<List<Stock>> ListAsync()
        {
            var stocks = _productContext.Stock.Include(s => s.Product);
            return await stocks.ToListAsync();
        }

        public async Task<List<Stock>> ListAsyncByProductId(int productId)
        {
            var stocks = _productContext.Stock.Include(s => s.Product)
                            .Where(s => s.ProductId == productId);
            return await stocks.ToListAsync();
        }

        public bool StockExists(int id)
        {
            return Stocks.Any(e => e.StockId == id);
        }

        public async Task UpdateAsync(Stock stock)
        {
            _productContext.Update(stock);
            await _productContext.SaveChangesAsync();
        }
    }
}
