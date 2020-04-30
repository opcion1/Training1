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
    public class ProductRepositoryTest : ProductDbContextTestBase<Product>
    {
        public ProductRepositoryTest()
            : base(new Product { Name = "product", Category = ProductCategory.Vegetable, Description = "Description" },
                    new Product { Name = "product 2", Category = ProductCategory.Fruit, Description = "Description 2" })
        {
        }

        public override EFRepositoryBase<Product> GetRepository(ProductContext context)
        {
            return new EFProductRepository(context);
        }

        internal override void AssertEntityEqual(Product entity, Product testModel)
        {
            Assert.Equal(entity.Name, testModel.Name);
            Assert.Equal(entity.Category, testModel.Category);
            Assert.Equal(entity.Description, testModel.Description);
        }

        internal override Product GetUpdatedEntity(Product testModel)
        {
            testModel.Name = _testModel2.Name;
            testModel.Description = _testModel2.Description;
            testModel.Category = _testModel2.Category;

            return testModel;
        }
    }
}
