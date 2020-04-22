using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Training1.Areas.Identity.Data;

namespace Training1.Tests.Mock.Identity
{
    public class MockUserManager : Mock<UserManager<AppUser>>
    {
        public MockUserManager()
            : base(new Mock<IUserStore<AppUser>>().Object, null, null, null, null, null, null, null, null)
        {
            this.Object.UserValidators.Add(new UserValidator<AppUser>());
            this.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());
        }

        public MockUserManager MockFindByIdAsync(AppUser appUser)
        {
            Setup(manager => manager.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(appUser);

            return this;
        }

        public MockUserManager MockUpdateAsync(IdentityResult result)
        {
            Setup(manager => manager.UpdateAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(result)
                .Verifiable();

            return this;
        }
    }
}
