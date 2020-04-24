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
    public class DayOfSesshinAccountantAuthorizationServiceTest
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly TestClaimsPrincipal _user;

        public DayOfSesshinAccountantAuthorizationServiceTest()
        {
            _authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AccountantAuthorizationHandler>();
            });
            _user = new TestClaimsPrincipal();
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinRead_WhenNotAccountant()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinRead_When_AccountantAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_DayOfSesshinRead_When_AccountantAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_DayOfSesshinRead_When_AccountantAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_WhenNotAccountant()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_When_AccountantAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_When_AccountantAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_When_AccountantAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_WhenNotAccountant()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_When_AccountantAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_When_AccountantAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_When_AccountantAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_WhenNotAccountant()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_When_AccountantAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_When_AccountantAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_When_AccountantAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserAccountantRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
    }
}

