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
    public class DayOfSesshinAdminAuthorizationServiceTest
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly TestClaimsPrincipal _user;

        public DayOfSesshinAdminAuthorizationServiceTest()
        {
            _authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            _user = new TestClaimsPrincipal();
        }


        [Fact]
        public async Task ShouldAllow_DayOfSesshinRead_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved); ;

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_DayOfSesshinRead_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted); ;

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinRead_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinRead_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_DayOfSesshinCreate_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_DayOfSesshinUpdate_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_DayOfSesshinDelete_WhenAdministratorAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_WhenAdministratorAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_WhenAdministratorAndSubmitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAdministratorsRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
    }
}
