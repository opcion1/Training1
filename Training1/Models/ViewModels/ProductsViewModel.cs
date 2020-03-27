

using System.Collections.Generic;

namespace Training1.Models.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products{ get; set; }
        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public string CategoryFilter { get; set; }
    }
}
