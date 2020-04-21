using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class StockService : ServiceBase<Stock>, IStockService
    {
        
        public StockService(IStockRepository stockRepository,
                                    IUnitOfWork unitOfWork)
            : base(stockRepository, unitOfWork)
        {
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync(int productId)
        {
            var stocks = await ((IStockRepository)_entityRepository).ListAsyncByProductId(productId);

            return stocks;
        }
    }
}
