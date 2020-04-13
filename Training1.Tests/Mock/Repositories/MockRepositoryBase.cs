using Moq;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Tests.Mock.Repositories
{
    public abstract class MockRepositoryBase<T> : Mock<IRepositoryBase<T>> where T : ModelBase
    {
        public MockRepositoryBase<T> MockAddAsync(T obj)
        {
            Setup(repo => repo.AddAsync(obj))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }
        public MockRepositoryBase<T> MockListAsync(T[] objs)
        {
            Setup(repo => repo.ListAsync())
                .ReturnsAsync(objs.Cast<T>().ToList());

            return this;
        }

        public MockRepositoryBase<T> MockGetByIdAsync(int id, T obj)
        {
            Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(obj);

            return this;
        }

        public MockRepositoryBase<T> MockDeleteAsync(int id)
        {
            Setup(repo => repo.DeleteAsync(id))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        public MockRepositoryBase<T> MockUpdate(T obj)
        {
            Setup(repo => repo.Update(obj))
                .Verifiable();

            return this;
        }
    }
}
