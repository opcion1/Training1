using Moq;
using Training1.Models.ViewModels;
using Training1.Services.Interfaces;

namespace Training1.Tests.Mock.Services
{
    public class MockAccountService : Mock<IAccountService>
    {
        public MockAccountService()
        {
        }

        public MockAccountService MockGetModelForAccountList()
        {
            Setup(service => service.GetModelForAccountList(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string>()))
                .ReturnsAsync(new AccountListViewModel());

            return this;
        }

        public MockAccountService MockGetEditAccount()
        {
            Setup(service => service.GetEditAccount(It.IsAny<string>()))
                .ReturnsAsync(new AccountEditViewModel());

            return this;
        }
    }
}
