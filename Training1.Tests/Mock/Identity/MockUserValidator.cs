using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Training1.Areas.Identity.Data;

namespace Training1.Tests.Mock.Identity
{
    public class MockUserValidator : Mock<IUserValidator<AppUser>>
    {
        public MockUserValidator()
        {
        }

        public MockUserValidator MockValidateAsync(IdentityResult result)
        {
            Setup(validator => validator.ValidateAsync(It.IsAny<UserManager<AppUser>>(), It.IsAny<AppUser>()))
                .ReturnsAsync(result);
            return this;
        }
    }
}
