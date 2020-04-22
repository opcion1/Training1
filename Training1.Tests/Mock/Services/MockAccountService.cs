using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
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

        public MockAccountService MockUpdateStatus()
        {
            Setup(service => service.UpdateAccountStatus(It.IsAny<string>(), It.IsAny<Status>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }

        public MockAccountService MockUpdateStatus_ReturnKeyNotFoundException()
        {
            Setup(service => service.UpdateAccountStatus(It.IsAny<string>(), It.IsAny<Status>()))
                .Throws(new KeyNotFoundException());
            return this;
        }

        public MockAccountService MockLogout()
        {
            Setup(service => service.LogOut())
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }
    }
}
