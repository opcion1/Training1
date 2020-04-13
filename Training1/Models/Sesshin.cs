using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Infrastructure;

namespace Training1.Models
{   
    public class Sesshin : ModelBase
    {
        [Column("SesshinId")]
        public override int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [GreaterThanStartDate]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name="Number of people")]
        public int NumberOfPeople { get; set; }
        [Required]
        [Display(Name="Tenzo")]
        public string AppUserId { get; set; }
        public ICollection<DayOfSesshin> Days { get; set; }
    }
    public class DayOfSesshin : ModelBase
    {
        [Column("DayOfSesshinId")]
        public override int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public ICollection<Meal> Meals { get; set; }
        [Required]
        public int NumberOfPeople { get; set; }
        public int SesshinId { get; set; }
        public Sesshin Sesshin { get; set; }
    }

    public class Meal : ModelBase
    {
        [Column("MealId")]
        public override int Id { get; set; }
        [Required]
        public MealType Type { get; set; }
        [Required]
        public IList<MealFood> MealFoods { get; set; }
        public int DayOfSesshinId { get; set; }
        public DayOfSesshin DayOfSesshin { get; set; }
    }
    public enum MealType
    {
        Breakfast,
        Genmai,
        Lunch,
        Diner
    }
    public class Food : ModelBase
    {
        [Column("FoodId")]
        public override int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Commentary { get; set; }
        [Required]
        [Display(Name = "Number of people")]
        public int NumberOfPeople { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public IList<MealFood> MealFoods { get; set; }
    }
    public class Ingredient : ModelBase
    {
        [Column("IngredientId")]
        public override int Id { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }
        [Required]
        public UnityType UnityType { get; set; }
    }
    public class MealFood
    {
        public int MealId { get; set; }
        public Meal Meal { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}
