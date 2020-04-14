using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockRepository _stockRepository;
        public IStockRepository Stock => _stockRepository;
        
        public StockService(IStockRepository stockRepository,
                                    IUnitOfWork unitOfWork)
        {
            _stockRepository = stockRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Stock stock)
        {
            await _stockRepository.AddAsync(stock);
            await _unitOfWork.CommitAsync();
        }

        public async Task EditAsync(Stock stock)
        {
            _stockRepository.Update(stock);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _stockRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync(int productId)
        {
            var stocks = await _stockRepository.ListAsyncByProductId(productId);

            return stocks;
        }
    }
}
