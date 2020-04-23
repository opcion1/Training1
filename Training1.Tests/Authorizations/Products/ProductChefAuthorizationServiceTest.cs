using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Models;
using Training1.Tests.Mock.Authorization;
using Training1.Tests.Mock.Identity;
using Training1.Tests.Mock.Services;
using Xunit;

namespace Training1.Tests.Authorizations.Products
{
    public class ProductChefAuthorizationServiceTest
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly TestClaimsPrincipal _user;

        public ProductChefAuthorizationServiceTest()
        {
            _authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler>(sp => new ChefAuthorizationHandler(new MockUserManager().Object, new MockMealService().Object, new MockSesshinService().Object));
            });
            _user = new TestClaimsPrincipal();
        }

        [Fact]
        public async Task ShouldNotAllow_ProductRead_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductRead_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_ProductRead_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_ProductRead_When_ChefAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductCreate_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductCreate_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_ProductCreate_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_ProductCreate_When_ChefAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductUpdate_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductUpdate_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_ProductUpdate_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_ProductUpdate_When_ChefAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductDelete_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductDelete_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_ProductDelete_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_ProductDelete_When_ChefAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
    }
}
