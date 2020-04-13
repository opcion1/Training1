using Moq;
using System;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;
using Training1.Tests.Mock.Repositories;

namespace Training1.Tests.Mock.Services
{
    public class MockProductService : Mock<IProductService>
    {
        private readonly MockProductRepository _mockRepo;
        private readonly MockConfiguration _mockConfig;
        public MockProductService()
        {
            _mockRepo = new MockProductRepository();
            _mockConfig = new MockConfiguration().MockGetValueInt("ItemsPerPage");

        }
        public MockProductService MockSearchSortAndPageProductAll(SearchSortPageResult<Product> results)
        {
            Setup(x => x.SearchSortAndPageProductAll(It.IsAny<SearchSortPageParameters>()))
                .ReturnsAsync(results);

            return this;
        }

        internal MockProductService MockCreateAsync(Product testProduct)
        {
            Setup(service => service.CreateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        internal MockProductService MockListAsync(Product[] products)
        {
            Setup(service => service.Product.ListAsync())
                .ReturnsAsync(products);

            return this;
        }

        internal MockProductService MockGetByIdAsync(int testProductId, Product testProduct)
        {
            Setup(service => service.Product.GetByIdAsync(testProductId))
                .ReturnsAsync(testProduct);

            return this;
        }

        internal MockProductService MockEditAsync(Product testProduct)
        {
            Setup(service => service.EditAsync(testProduct))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        internal MockProductService MockDeleteAsync(int testProductId)
        {
            Setup(service => service.DeleteAsync(testProductId))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        internal MockProductService VerifySearchSortAndPageProductAll(Func<Times> times)
        {
            Verify(x => x.SearchSortAndPageProductAll(It.IsAny<SearchSortPageParameters>()), times);

            return this;
        }

        internal MockProductService VerifyCreateAsync(Func<Times> times)
        {
            Verify(x => x.CreateAsync(It.IsAny<Product>()), times);

            return this;
        }

        internal MockProductService VerifyGetByIdAsync(Func<Times> times)
        {
            Verify(x => x.Product.GetByIdAsync(It.IsAny<int>()), times);

            return this;
        }

        internal MockProductService VerifyEditAsync(Func<Times> times)
        {
            Verify(x => x.EditAsync(It.IsAny<Product>()), times);

            return this;
        }

        internal MockProductService VerifyDeleteAsync(Func<Times> times)
        {
            Verify(x => x.DeleteAsync(It.IsAny<int>()), times);

            return this;
        }
    }
}
