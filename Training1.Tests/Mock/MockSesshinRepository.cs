using Moq;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;

namespace Training1.Tests.Mock
{
    class MockSesshinRepository : Mock<ISesshinRepository>
    {
        public MockSesshinRepository MockAddAsync(Sesshin sesshin)
        {
            Setup(repo => repo.AddAsync(sesshin))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }
        public MockSesshinRepository MockListAsync(Sesshin[] sesshins)
        {
            this.Setup(repo => repo.ListAsync()).ReturnsAsync(sesshins.Cast<Sesshin>().ToList());

            return this;
        }

        public MockSesshinRepository MockGetByIdAsync(int id, Sesshin sesshin)
        {
            Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(sesshin);
            return this;
        }

        public MockSesshinRepository MockDeleteAsync(int id)
        {
            Setup(repo => repo.DeleteAsync(id))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }
    }
}
