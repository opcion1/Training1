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
    public class MealFoodAdminAuthorizationServiceTest
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly TestClaimsPrincipal _user;

        public MealFoodAdminAuthorizationServiceTest()
        {
            _authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            _user = new TestClaimsPrincipal();
        }


        [Fact]
        public async Task ShouldAllow_MealFoodRead_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved); ;

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_MealFoodRead_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted); ;

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodRead_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodRead_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_MealFoodCreate_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodCreate_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodCreate_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodCreate_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_MealFoodUpdate_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodUpdate_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodUpdate_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodUpdate_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_MealFoodDelete_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Delete);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodDelete_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodDelete_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_MealFoodDelete_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new MealFood(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
    }
}
