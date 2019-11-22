

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Training1.Authorization;
using Training1.Models;
using Training1.Repositories;

namespace Training1.ViewComponents
{
    public class StockListViewComponent : ViewComponent
    {
        private readonly IStockRepository _stockRepository;
        public StockListViewComponent(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            int productId)
        {
            var stocks = await _stockRepository.ListAsyncByProductId(productId);
            return View(stocks);
        }

    }
}
