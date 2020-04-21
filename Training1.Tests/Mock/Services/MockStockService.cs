using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Services.Interfaces;

namespace Training1.Tests.Mock.Services
{
    public class MockStockService : Mock<IStockService>
    {
        public MockStockService()
        {
        }

        internal MockStockService MockCreateAsync(Stock testStock)
        {
            Setup(service => service.CreateAsync(testStock))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        internal MockStockService MockGetStocksAsync(int productId, Stock[] stocks)
        {
            Setup(service => service.GetStocksAsync(productId))
                .ReturnsAsync(stocks);

            return this;
        }

        internal MockStockService MockListAsync(Stock[] stocks)
        {
            Setup(service => service.ListAsync())
                .ReturnsAsync(stocks);

            return this;
        }

        internal MockStockService MockGetByIdAsync(int testStockId, Stock testStock)
        {
            Setup(service => service.GetByIdAsync(testStockId))
                .ReturnsAsync(testStock);

            return this;
        }

        internal MockStockService MockEditAsync(Stock testStock)
        {
            Setup(service => service.EditAsync(testStock))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        internal MockStockService MockDeleteAsync(int testStockId)
        {
            Setup(service => service.DeleteAsync(testStockId))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        internal MockStockService VerifyCreateAsync(Func<Times> times)
        {
            Verify(x => x.CreateAsync(It.IsAny<Stock>()), times);

            return this;
        }

        internal MockStockService VerifyGetByIdAsync(Func<Times> times)
        {
            Verify(x => x.GetByIdAsync(It.IsAny<int>()), times);

            return this;
        }

        internal MockStockService VerifyEditAsync(Func<Times> times)
        {
            Verify(x => x.EditAsync(It.IsAny<Stock>()), times);

            return this;
        }

        internal MockStockService VerifyDeleteAsync(Func<Times> times)
        {
            Verify(x => x.DeleteAsync(It.IsAny<int>()), times);

            return this;
        }
    }
}
