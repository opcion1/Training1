using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training1.Tests.Mock
{
    public class MockConfiguration : Mock<IConfiguration>
    {
        public MockConfiguration MockGetValueInt(string key)
        {
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value).Returns("10");

            Setup(conf => conf.GetSection(key))
                .Returns(configurationSection.Object);
            return this;
        }
    }
}
