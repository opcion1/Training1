using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;
using Training1.Repositories.Interfaces;
using Xunit;

namespace Training1.Tests.Repositories
{
    public class DayOfSesshinRepositoryTest : ProductDbContextTestBase<DayOfSesshin>
    {
        public DayOfSesshinRepositoryTest()
            : base(new DayOfSesshin { Date = DateTime.Today.AddDays(-7), NumberOfPeople = 50, Sesshin = new Sesshin { Name = "DayOfSesshin 1", Description = "Description 1", StartDate = DateTime.Today.AddDays(-7), EndDate = DateTime.Today.AddDays(-6), NumberOfPeople = 50, AppUserId = "AppUserId" } } ,
                    new DayOfSesshin { Date = DateTime.Today.AddDays(-10), NumberOfPeople = 60, Sesshin = new Sesshin { Name = "DayOfSesshin 2", Description = "Description 2", StartDate = DateTime.Today.AddDays(10), EndDate = DateTime.Today.AddDays(12), NumberOfPeople = 60, AppUserId = "AppUserId" } })
        {
        }

        [Fact]
        public async Task Entity_ListAsyncBySesshinId_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Add an identity to the in memory database
                AddEntities(new List<ModelBase> { _testModel, _testModel2 }, options);

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = new EFDayOfSesshinRepository(context);
                    IEnumerable<DayOfSesshin> days = await repository.ListAsyncBySesshinId(1) ;
                    Assert.Equal(1, ((ICollection<DayOfSesshin>)days).Count);
                }
            }
            finally
            {
                _connection.Close();
            }
        }
        [Fact]
        public async Task Entity_UpdateNumberOfPeopleAsync_Test()
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
                    var repository = new EFDayOfSesshinRepository(context);
                    DayOfSesshin day= await context.DaysOfSesshin.SingleAsync();
                    await repository.UpdateNumberOfPeopleAsync(1, 70);
                    await context.SaveChangesAsync();
                }

                //Assert
                using (var context = new ProductContext(options))
                {
                    DayOfSesshin day = await context.DaysOfSesshin.SingleAsync();
                    Assert.Equal(70, day.NumberOfPeople);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        #region override method
        public override EFRepositoryBase<DayOfSesshin> GetRepository(ProductContext context)
        {
            return new EFDayOfSesshinRepository(context);
        }

        internal override void AssertEntityEqual(DayOfSesshin entity1, DayOfSesshin entity2)
        {
            Assert.Equal(entity1.Date, entity2.Date);
            Assert.Equal(entity1.NumberOfPeople, entity2.NumberOfPeople);
            Assert.Equal(entity1.SesshinId, entity2.SesshinId);
        }

        internal override DayOfSesshin GetUpdatedEntity(DayOfSesshin entity)
        {
            entity.Date = _testModel2.Date;
            entity.NumberOfPeople = _testModel2.NumberOfPeople;
            entity.SesshinId = _testModel2.SesshinId;

            return entity;
        }
        #endregion
    }
}
