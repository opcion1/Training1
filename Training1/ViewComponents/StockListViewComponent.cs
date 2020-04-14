using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Training1.Models.ViewModels;
using Training1.Services.Interfaces;

namespace Training1.ViewComponents
{
    public class StockListViewComponent : ViewComponent
    {
        private readonly IStockService _stockService;
        public StockListViewComponent(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            int productId)
        {
            var stocks = await _stockService.GetStocksAsync(productId);
            var stockList = new StockListViewModel()
            {
                Stocks = stocks,
                ProductId = productId
            };
            return View(stockList);
        }

    }
}
