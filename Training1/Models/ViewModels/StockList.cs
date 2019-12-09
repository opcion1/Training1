

using System.Collections.Generic;

namespace Training1.Models
{
    public class StockList
    {
        public IEnumerable<Stock> Stocks { get; internal set; }
        public int ProductId { get; internal set; }
    }
}
