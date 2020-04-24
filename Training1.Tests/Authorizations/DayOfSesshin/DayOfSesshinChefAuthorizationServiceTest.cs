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
    public class DayOfSesshinChefAuthorizationServiceTest
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly TestClaimsPrincipal _user;
        private readonly MockUserManager _userManager;
        private readonly MockMealService _mealService;
        private readonly MockSesshinService _sesshinService;

        public DayOfSesshinChefAuthorizationServiceTest()
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
        public async Task ShouldNotAllow_DayOfSesshinRead_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinRead_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_DayOfSesshinRead_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_DayOfSesshinRead_When_ChefAndApproved()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinCreate_When_ChefAndApprovedAndNotOwner()
        {
            //Arrange
            string testUserId1 = "testUserId1";
            string testUserId2 = "testUserId2";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _sesshinService.MockGetSesshinOwner(testUserId1);
            _userManager.MockGetUserId(testUserId2);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
            _sesshinService.Verify();
            _userManager.Verify();
        }


        [Fact]
        public async Task ShouldAllow_DayOfSesshinCreate_When_ChefAndApprovedAndOwner()
        {
            //Arrange
            string testUserId = "testUserId";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _sesshinService.MockGetSesshinOwner(testUserId);
            _userManager.MockGetUserId(testUserId);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
            _sesshinService.Verify();
            _userManager.Verify();
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinUpdate_When_ChefAndApprovedAndNotOwner()
        {
            //Arrange
            string testUserId1 = "testUserId1";
            string testUserId2 = "testUserId2";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _sesshinService.MockGetSesshinOwner(testUserId1);
            _userManager.MockGetUserId(testUserId2);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
            _sesshinService.Verify();
            _userManager.Verify();
        }


        [Fact]
        public async Task ShouldAllow_DayOfSesshinUpdate_When_ChefAndApprovedAndOwner()
        {
            //Arrange
            string testUserId = "testUserId";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _sesshinService.MockGetSesshinOwner(testUserId);
            _userManager.MockGetUserId(testUserId);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
            _sesshinService.Verify();
            _userManager.Verify();
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_WhenNotChef()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_When_ChefAndRejected()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Rejected);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_When_ChefAndSumitted()
        {
            //Arrange
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Submitted);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldNotAllow_DayOfSesshinDelete_When_ChefAndApprovedAndNotOwner()
        {
            //Arrange
            string testUserId1 = "testUserId1";
            string testUserId2 = "testUserId2";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _sesshinService.MockGetSesshinOwner(testUserId1);
            _userManager.MockGetUserId(testUserId2);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
            _sesshinService.Verify();
            _userManager.Verify();
        }


        [Fact]
        public async Task ShouldAllow_DayOfSesshinDelete_When_ChefAndApprovedAndOwner()
        {
            //Arrange
            string testUserId = "testUserId";
            _user
                .AddRole(Constants.UserChefRole)
                .AddStatus(Status.Approved);
            _sesshinService.MockGetSesshinOwner(testUserId);
            _userManager.MockGetUserId(testUserId);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new DayOfSesshin(), UserOperations.Delete);

            // Assert
            Assert.True(allowed.Succeeded);
            _sesshinService.Verify();
            _userManager.Verify();
        }
    }
}
