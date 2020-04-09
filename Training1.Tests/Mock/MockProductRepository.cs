using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;

namespace Training1.Tests.Mock
{
    class MockProductRepository : Mock<IProductRepository>
    {
        public MockProductRepository MockAddAsync(Product product)
        {
            Setup(repo => repo.AddAsync(product))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }
        public MockProductRepository MockListAsync(Product[] products)
        {
            this.Setup(repo => repo.ListAsync()).ReturnsAsync(products.Cast<Product>().ToList());

            return this;
        }

        public MockProductRepository MockGetByIdAsync(int id, Product product)
        {
            Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(product);
            return this;
        }

        public MockProductRepository MockDeleteAsync(int id)
        {
            Setup(repo => repo.DeleteAsync(id))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }

        public MockProductRepository MockUpdateAsync(Product product)
        {
            Setup(repo => repo.UpdateAsync(product))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }
    }
}
