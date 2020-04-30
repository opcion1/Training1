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
    public class SesshinRepositoryTest : ProductDbContextTestBase<Sesshin>
    {
        public SesshinRepositoryTest()
            : base(new Sesshin { Name = "Sesshin 1", Description = "Description 1", StartDate = DateTime.Today.AddDays(-7), EndDate = DateTime.Today.AddDays(-6), NumberOfPeople = 50, AppUserId = "AppUserId"},
                    new Sesshin { Name = "Sesshin 2", Description = "Description 2", StartDate = DateTime.Today.AddDays(10), EndDate = DateTime.Today.AddDays(12), NumberOfPeople = 60, AppUserId = "AppUserId" })
        {
        }

        #region override method
        public override EFRepositoryBase<Sesshin> GetRepository(ProductContext context)
        {
            return new EFSesshinRepository(context);
        }

        internal override void AssertEntityEqual(Sesshin entity1, Sesshin entity2)
        {
            Assert.Equal(entity1.Name, entity2.Name);
            Assert.Equal(entity1.Description, entity2.Description);
            Assert.Equal(entity1.StartDate, entity2.StartDate);
            Assert.Equal(entity1.EndDate, entity2.EndDate);
            Assert.Equal(entity1.NumberOfPeople, entity2.NumberOfPeople);
            Assert.Equal(entity1.AppUserId, entity2.AppUserId);
        }

        internal override Sesshin GetUpdatedEntity(Sesshin entity)
        {
            entity.Name = _testModel2.Name;
            entity.Description = _testModel2.Description;
            entity.StartDate = _testModel2.StartDate;
            entity.EndDate = _testModel2.EndDate;
            entity.NumberOfPeople = _testModel2.NumberOfPeople;
            entity.AppUserId = _testModel2.AppUserId;

            return entity;
        }
        #endregion
    }
}
