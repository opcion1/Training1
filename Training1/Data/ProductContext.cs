using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Training1.Models;

namespace Training1.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {

        }

        public ProductContext (DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealFood>().HasKey(sc => new { sc.MealId, sc.FoodId });
        }

        public DbSet<Training1.Models.Product> Product { get; set; }
        public DbSet<Training1.Models.Stock> Stock { get; set; }
        public DbSet<Training1.Models.Sesshin> Sesshin { get; set; }
        public DbSet<Training1.Models.DayOfSesshin> DaysOfSesshin { get; set; }
        public DbSet<Training1.Models.Meal> Meal { get; set; }
        public DbSet<Training1.Models.Food> Food { get; set; }
        public DbSet<MealFood> MealFood { get; set; }
        public DbSet<Training1.Models.Ingredient> Ingredient { get; set; }
    }
}
