

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Training1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ProductContext>>()))
            {
                // Look for any movies.
                if (context.Product.Any())
                {
                    return;   // DB has been seeded
                }

                context.Product.AddRange(
                    new Product
                    {
                        Name = "Carots",
                        Description = "Vegetable orange, nice for salads",
                        Category = ProductCategory.Vegetable
                    },

                    new Product
                    {
                        Name = "Potatoes",
                        Description = "French fries baby!!!!!!!!!",
                        Category = ProductCategory.Vegetable
                    },

                    new Product
                    {
                        Name = "Apple",
                        Description = "Apple Pie, hummmmmmmmmmmmmmmmmy",
                        Category = ProductCategory.Fruit
                    },

                    new Product
                    {
                        Name = "Coffee",
                        Description = "I want my coffee!!!!!!!!",
                        Category = ProductCategory.Coffee
                    },

                    new Product
                    {
                        Name = "Riz",
                        Description = "My favorite cereal",
                        Category = ProductCategory.Cereal
                    },

                    new Product
                    {
                        Name = "Quinoa",
                        Description = "Gluten freeeeeeeeeeeeeeeeeeeeeeeeeee",
                        Category = ProductCategory.Cereal
                    },

                    new Product
                    {
                        Name = "Mustard",
                        Description = "Warning, very spicy!!!!!!!!!!!!",
                        Category = ProductCategory.Condiment
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
