

using System;
using System.ComponentModel.DataAnnotations;

namespace Training1.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public UnityType UnityType { get; set; }
        public decimal? PricePorUnity { get; set; }
        public Currency Currency { get; set; }
        [DataType(DataType.Date)]
        public DateTime CommandDate { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
    public enum UnityType
    {
        Grammes,
        Kilogrammes,
        Liter,
        Unity
    }
    public enum Currency
    {
        US_Dollar,
        Euro,
        Peso_Argentino,
        Peso_Chileno
    }
}
