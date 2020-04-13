
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training1.Models
{
    public class Stock : ModelBase
    {
        [Column("StockId")]
        public override int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }
        [Required]
        public UnityType UnityType { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name ="Price por unity")]
        public decimal? PricePorUnity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Total price")]
        public decimal? TotalPrice { get; set; }
        public Currency Currency { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Command date")]
        public DateTime CommandDate { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
    public enum UnityType
    {
        [Description("g")]
        Grammes,
        [Description("kg")]
        Kilogrammes,
        [Description("l")]
        Liter,
        [Description("U")]
        Unity
    }
    public enum Currency
    {
        [Description("$")]
        US_Dollar,
        [Description("€")]
        Euro,
        [Description("$ Arg")]
        Peso_Argentino,
        [Description("$ Chi")]
        Peso_Chileno
    }
}
