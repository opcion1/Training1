using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Services.Interfaces;

namespace Training1.Tests.Mock.Services
{
    public class MockHomeService : Mock<IHomeService>
    {
        public MockHomeService MockUpdateAppStyle()
        {
            Setup(x => x.UpdateAppStyle(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }
        public MockHomeService MockUpdateAppStyleAndThrowKeyNotFoundException()
        {
            Setup(x => x.UpdateAppStyle(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new KeyNotFoundException());
            
            return this;
        }

        public MockHomeService VerifyUpdateAppStyle(Func<Times> times)
        {
            Verify(x => x.UpdateAppStyle(It.IsAny<string>(), It.IsAny<string>()), times);

            return this;
        }
    }
}
