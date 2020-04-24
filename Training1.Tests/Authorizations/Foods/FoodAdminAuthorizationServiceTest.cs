using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Authorization;
using Training1.Tests.Mock.Authorization;
using Xunit;
using Training1.Areas.Identity.Data;

namespace Training1.Tests.Authorizations
{
    public class FoodAdminAuthorizationServiceTest
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly TestClaimsPrincipal _user;

        public FoodAdminAuthorizationServiceTest()
        {
            _authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            _user = new TestClaimsPrincipal();
        }


        [Fact]
        public async Task ShouldAllow_FoodRead_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved); ;

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_FoodRead_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted); ;

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodRead_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodRead_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_FoodCreate_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodCreate_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodCreate_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodCreate_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_FoodUpdate_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodUpdate_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodUpdate_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodUpdate_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_FoodDelete_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Delete);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodDelete_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodDelete_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_FoodDelete_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Food(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
    }
}
