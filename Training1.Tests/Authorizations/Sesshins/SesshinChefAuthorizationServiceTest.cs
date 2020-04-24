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

namespace Training1.Tests.Authorizations
{
    public class SesshinChefAuthorizationServiceTest
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly TestClaimsPrincipal _user;
        private readonly MockUserManager _userManager;
        private readonly MockMealService _mealService;
        private readonly MockSesshinService _sesshinService;

        public SesshinChefAuthorizationServiceTest()
        {
            _userManager = new MockUserManager();
            _mealService = new MockMealService();
            _sesshinService = new MockSesshinService();

            _authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler>(sp => new ChefAuthorizationHandler(_userManager.Object, _mealService.Object, _sesshinService.Object));
            });
            _user = new TestClaimsPrincipal();
        }

        [Fact]
        public async Task ShouldNotAllow_SesshinRead_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_SesshinRead_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_SesshinRead_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_SesshinRead_When_ChefAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_SesshinCreate_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_SesshinCreate_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_SesshinCreate_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_SesshinCreate_When_ChefAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_SesshinUpdate_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_SesshinUpdate_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_SesshinUpdate_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_SesshinUpdate_When_ChefAndApprovedAndNotOwner()
        {
            //Arrange
            string testUserId = "testUserId";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _userManager.MockGetUserId(testUserId);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin() { AppUserId = "AnOtherId"}, UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
            _userManager.Verify();
        }

        [Fact]
        public async Task ShouldAllow_SesshinUpdate_When_ChefAndApprovedAndOwner()
        {
            //Arrange
            string testUserId = "testUserId";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _userManager.MockGetUserId(testUserId);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin() { AppUserId = testUserId }, UserOperations.Update); 

            // Assert
            Assert.True(allowed.Succeeded);
            _userManager.Verify();
        }

        [Fact]
        public async Task ShouldNotAllow_SesshinDelete_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_SesshinDelete_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_SesshinDelete_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_SesshinDelete_When_ChefAndApprovedAndNotOwner()
        {
            //Arrange
            string testUserId = "testUserId";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _userManager.MockGetUserId(testUserId);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin() { AppUserId = "AnOtherId" }, UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
            _userManager.Verify();
        }

        [Fact]
        public async Task ShouldAllow_SesshinDelete_When_ChefAndApprovedAndOwner()
        {
            //Arrange
            string testUserId = "testUserId";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _userManager.MockGetUserId(testUserId);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Sesshin() { AppUserId = testUserId }, UserOperations.Delete);

            // Assert
            Assert.True(allowed.Succeeded);
            _userManager.Verify();
        }
    }
}
