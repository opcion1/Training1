using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Models;
using Training1.Tests.Mock.Authorization;
using Xunit;

namespace Training1.Tests.Authorizations
{
    public class ProductAccountantAuthorizationServiceTest
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly TestClaimsPrincipal _user;

        public ProductAccountantAuthorizationServiceTest()
        {
            _authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AccountantAuthorizationHandler>();
            });
            _user = new TestClaimsPrincipal();
        }

        [Fact]
        public async Task ShouldNotAllow_ProductRead_WhenNotAccountant()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductRead_When_AccountantAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_ProductRead_When_AccountantAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_ProductRead_When_AccountantAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductCreate_WhenNotAccountant()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductCreate_When_AccountantAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_ProductCreate_When_AccountantAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_ProductCreate_When_AccountantAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductUpdate_WhenNotAccountant()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductUpdate_When_AccountantAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_ProductUpdate_When_AccountantAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_ProductUpdate_When_AccountantAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductDelete_WhenNotAccountant()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductDelete_When_AccountantAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_ProductDelete_When_AccountantAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_ProductDelete_When_AccountantAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
    }
}
