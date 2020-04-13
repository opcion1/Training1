using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Training1.Models
{
    public class Product : ModelBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public ProductCategory Category { get; set; }
        [Display(Name="Tell me more")]
        public string Description { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }

    public enum ProductCategory
    {
        Vegetable,
        Fruit,
        Cereal,
        Coffee,
        Condiment, 
        Legume,
        Else
    }
}
