﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Infrastructure;

namespace Training1.Models
{    public class Sesshin
    {
        public int SesshinId { get; set; }
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
        public IEnumerable<DayOfSesshin> Days { get; set; }
    }
    public class DayOfSesshin
    {
        public int DayOfSesshinId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public IEnumerable<Meal> Meals { get; set; }
        [Required]
        public int NumberOfPeople { get; set; }
        public int SesshinId { get; set; }
        public Sesshin Sesshin { get; set; }
    }

    public class Meal
    {
        public int MealId { get; set; }
        [Required]
        public MealType Type { get; set; }
        [Required]
        public IEnumerable<Food> Foods { get; set; }
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
    public class Food
    {
        public int FoodId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Commentary { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
    public class Ingredient
    {
        public int IngredientId { get; set; }
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
}
