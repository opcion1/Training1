﻿

using System.Collections.Generic;

namespace Training1.Models.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products{ get; set; }
        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ProductCategory? CategoryFilter { get; set; }
        public string CurrentSort { get; set; }
        public string NameSort { get; set; }
        public string CategorySort { get; set; }
    }
}
