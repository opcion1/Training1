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
    public class StockRepositoryTest : ModelBaseTestBase<Stock>
    {
        public StockRepositoryTest()
            : base(new Stock { Quantity = 1, UnityType = UnityType.Grammes, PricePorUnity = 1, TotalPrice = 1, CommandDate = DateTime.Today, Product = new Product { Name = "product", Category = ProductCategory.Vegetable, Description = "Description" } },
                    new Stock { Quantity = 2, UnityType = UnityType.Kilogrammes, PricePorUnity = 2, TotalPrice = 4, CommandDate = DateTime.Today.AddDays(7), Product = new Product { Name = "product 2", Category = ProductCategory.Fruit, Description = "Description 2" } })
        {
        }

        [Fact]
        public async Task Entity_ListAsyncByProductId_Test()
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
                    EFStockRepository repository = new EFStockRepository(context);
                    ICollection<Stock> stocks = await repository.ListAsyncByProductId(1);
                    Assert.Equal(1, stocks.Count);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        #region override method
        public override EFRepositoryBase<Stock> GetRepository(ProductContext context)
        {
            return new EFStockRepository(context);
        }

        internal override void AssertEntityEqual(Stock entity1, Stock entity2)
        {
            Assert.Equal(entity1.Quantity, entity2.Quantity);
            Assert.Equal(entity1.UnityType, entity2.UnityType);
            Assert.Equal(entity1.PricePorUnity, entity2.PricePorUnity);
            Assert.Equal(entity1.CommandDate, entity2.CommandDate);
            Assert.Equal(entity1.TotalPrice, entity2.TotalPrice);
        }

        internal override Stock GetUpdatedEntity(Stock entity)
        {
            entity.Quantity = _testModel2.Quantity;
            entity.UnityType = _testModel2.UnityType;
            entity.PricePorUnity = _testModel2.PricePorUnity;
            entity.CommandDate = _testModel2.CommandDate;
            entity.TotalPrice = _testModel2.TotalPrice;

            return entity;
        }
        #endregion
    }
}
