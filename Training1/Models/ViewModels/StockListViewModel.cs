

using System.Collections.Generic;

namespace Training1.Models.ViewModels
{
    public class StockListViewModel
    {
        public IEnumerable<Stock> Stocks { get; internal set; }
        public int ProductId { get; internal set; }
    }
}
