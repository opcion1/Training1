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

        internal override void AssertEntityEqual(Product entity1, Product entity2)
        {
            Assert.Equal(entity1.Name, entity2.Name);
            Assert.Equal(entity1.Category, entity2.Category);
            Assert.Equal(entity1.Description, entity2.Description);
        }

        internal override Product GetUpdatedEntity(Product entity)
        {
            entity.Name = _testModel2.Name;
            entity.Description = _testModel2.Description;
            entity.Category = _testModel2.Category;

            return entity;
        }
    }
}
