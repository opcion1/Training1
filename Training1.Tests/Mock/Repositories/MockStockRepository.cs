using Moq;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;

namespace Training1.Tests.Mock.Repositories
{
    class MockStockRepository : Mock<IStockRepository>
    {
        public MockStockRepository MockAddAsync(Stock stock)
        {
            Setup(repo => repo.AddAsync(stock))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }
        public MockStockRepository MockListAsync(Stock[] stocks)
        {
            this.Setup(repo => repo.ListAsync()).ReturnsAsync(stocks.Cast<Stock>().ToList());

            return this;
        }

        public MockStockRepository MockGetByIdAsync(int id, Stock stock)
        {
            Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(stock);
            return this;
        }

        public MockStockRepository MockDeleteAsync(int id)
        {
            Setup(repo => repo.DeleteAsync(id))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }
    }
}
