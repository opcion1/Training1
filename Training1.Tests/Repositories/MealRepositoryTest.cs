using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;
using Training1.Repositories.Interfaces;
using Training1.Tests.Extensions;
using Xunit;

namespace Training1.Tests.Repositories
{
    public class MealRepositoryTest : ModelBaseTestBase<Meal>
    {
        private IList<Sesshin> _listSesshin = new List<Sesshin>();
        private readonly Sesshin _testSesshin = new Sesshin {Id = 1, Name = "DayOfSesshin 1", Description = "Description 1", StartDate = DateTime.Today.AddDays(-7), EndDate = DateTime.Today.AddDays(-6), NumberOfPeople = 50, AppUserId = "AppUserId" };
        public MealRepositoryTest()
            : base(new Meal { Type = MealType.Lunch, DayOfSesshin = new DayOfSesshin { Date = DateTime.Today.AddDays(-7), NumberOfPeople = 50, Sesshin = new Sesshin { Name = "DayOfSesshin 1", Description = "Description 1", StartDate = DateTime.Today.AddDays(-7), EndDate = DateTime.Today.AddDays(-6), NumberOfPeople = 50, AppUserId = "AppUserId" } } },
                    new Meal { Type = MealType.Diner, DayOfSesshin = new DayOfSesshin { Date = DateTime.Today.AddDays(-7), NumberOfPeople = 50, Sesshin = new Sesshin { Name = "DayOfSesshin 2", Description = "Description 2", StartDate = DateTime.Today.AddDays(-10), EndDate = DateTime.Today.AddDays(-8), NumberOfPeople = 50, AppUserId = "AppUserId" } } })
        {
        }

        [Fact]
        public async Task Entity_GetMealSesshinOwner_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Add an identity to the in memory database
                AddEntities(new List<ModelBase> { _testModel }, options);

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = new EFMealRepository(context);
                    _listSesshin.Add(_testSesshin);
                    context.Sesshin = context.Sesshin.MockFromSql(_listSesshin.AsQueryable());
                    string sesshinOwner = repository.GetMealSesshinOwner(1);
                    Assert.Equal("AppUserId", sesshinOwner);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        #region override method
        public override EFRepositoryBase<Meal> GetRepository(ProductContext context)
        {
            return new EFMealRepository(context);
        }

        internal override void AssertEntityEqual(Meal entity1, Meal entity2)
        {
            Assert.Equal(entity1.Type, entity2.Type);
        }

        internal override Meal GetUpdatedEntity(Meal entity)
        {
            entity.Type = _testModel2.Type;

            return entity;
        }
        #endregion
    }
}
