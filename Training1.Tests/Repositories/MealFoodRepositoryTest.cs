using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;
using Xunit;

namespace Training1.Tests.Repositories
{
    public class MealFoodRepositoryTest : ProductDbContextTestBase
    {
        private readonly MealFood _testMealFood;

        public MealFoodRepositoryTest() : base()
        {
            _testMealFood = new MealFood
            {
                Food = new Food { Name = "Ratatouille" },
                Meal = new Meal { Type = MealType.Lunch, DayOfSesshin = new DayOfSesshin { Date = DateTime.Today.AddDays(-7), NumberOfPeople = 50, Sesshin = new Sesshin { Name = "DayOfSesshin 1", Description = "Description 1", StartDate = DateTime.Today.AddDays(-7), EndDate = DateTime.Today.AddDays(-6), NumberOfPeople = 50, AppUserId = "AppUserId" } } }
            };
        }




        [Fact]
        public async Task AddAsync_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = new EFMealFoodRepository(context);
                    await repository.AddAsync(_testMealFood);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProductContext(options))
                {
                    var mealFoods = context.MealFood;
                    var entityCount = await mealFoods.CountAsync();
                    var mf = await mealFoods.SingleAsync();
                    Assert.Equal(1, entityCount);
                    AssertEntityEqual(mf, _testMealFood);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        [Fact]
        public async Task DeleteByMealIdAsync_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Add an identity to the in memory database
                using (var context = new ProductContext(options))
                {
                    context.MealFood.Add(_testMealFood);
                    await context.SaveChangesAsync();
                }

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = new EFMealFoodRepository(context);
                    await repository.DeleteByMealIdAsync(1);
                    await context.SaveChangesAsync();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProductContext(options))
                {
                    var entityCount = await context.MealFood.CountAsync();
                    Assert.Equal(0, entityCount);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        [Fact]
        public async Task DeleteAsync_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Add an identity to the in memory database
                using (var context = new ProductContext(options))
                {
                    context.MealFood.Add(_testMealFood);
                    await context.SaveChangesAsync();
                }

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = new EFMealFoodRepository(context);
                    await repository.DeleteAsync(1, 1);
                    await context.SaveChangesAsync();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProductContext(options))
                {
                    var entityCount = await context.MealFood.CountAsync();
                    Assert.Equal(0, entityCount);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        private void AssertEntityEqual(MealFood mf, MealFood testMealFood)
        {
            Assert.Equal(mf.MealId, testMealFood.MealId);
            Assert.Equal(mf.FoodId, testMealFood.FoodId);
        }
    }
}
