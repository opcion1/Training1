using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;
using Training1.Tests.Mock.Repositories;

namespace Training1.Tests.Mock.Services
{
    public class MockSesshinService : Mock<ISesshinService>
    {
        private readonly MockSesshinRepository _mockRepo;
        public MockSesshinRepository Sesshin { get { return _mockRepo; } }
        public MockSesshinService()
        {
            _mockRepo = new MockSesshinRepository();
        }

        internal MockSesshinService MockCreateAsync(Sesshin testSesshin)
        {
            Setup(service => service.CreateAsync(It.IsAny<Sesshin>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        internal MockSesshinService MockListAsync(Sesshin[] products)
        {
            Setup(service => service.ListAsync())
                .ReturnsAsync(products);

            return this;
        }

        internal MockSesshinService MockGetByIdAsync(int testSesshinId, Sesshin testSesshin)
        {
            Setup(service => service.GetByIdAsync(testSesshinId))
                .ReturnsAsync(testSesshin);

            return this;
        }

        internal MockSesshinService MockEditAsync(Sesshin testSesshin)
        {
            Setup(service => service.EditAsync(testSesshin))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        internal MockSesshinService MockDeleteAsync(int testSesshinId)
        {
            Setup(service => service.DeleteAsync(testSesshinId))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        internal MockSesshinService MockGetSesshinOwner(string userId)
        {
            Setup(service => service.GetSesshinOwner(It.IsAny<int>()))
                .Returns(userId)
                .Verifiable();

            return this;
        }

        internal MockSesshinService VerifyCreateAsync(Func<Times> times)
        {
            Verify(x => x.CreateAsync(It.IsAny<Sesshin>()), times);

            return this;
        }

        internal MockSesshinService VerifyGetByIdAsync(Func<Times> times)
        {
            Verify(x => x.GetByIdAsync(It.IsAny<int>()), times);

            return this;
        }

        internal MockSesshinService VerifyEditAsync(Func<Times> times)
        {
            Verify(x => x.EditAsync(It.IsAny<Sesshin>()), times);

            return this;
        }

        internal MockSesshinService VerifyDeleteAsync(Func<Times> times)
        {
            Verify(x => x.DeleteAsync(It.IsAny<int>()), times);

            return this;
        }
    }
}
