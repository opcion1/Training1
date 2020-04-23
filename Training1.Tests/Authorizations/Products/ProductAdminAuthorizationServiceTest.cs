using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Authorization;
using Training1.Tests.Mock.Authorization;
using Xunit;

namespace Training1.Tests.Authorizations.Products
{
    public class ProductAdminAuthorizationServiceTest
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly TestClaimsPrincipal _user;

        public ProductAdminAuthorizationServiceTest()
        {
            _authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            _user = new TestClaimsPrincipal();
        }


        [Fact]
        public async Task ShouldAllow_ProductRead_WhenAdministrator()
        {
            //Arrange
            _user.AddRole(Constants.UserAdministratorsRole);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductRead_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Read);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_ProductCreate_WhenAdministrator()
        {
            //Arrange
            _user.AddRole(Constants.UserAdministratorsRole);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductCreate_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Create);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldAllow_ProductUpdate_WhenAdministrator()
        {
            //Arrange
            _user.AddRole(Constants.UserAdministratorsRole);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductUpdate_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Update);

            // Assert
            Assert.False(allowed.Succeeded);
        }


        [Fact]
        public async Task ShouldAllow_ProductDelete_WhenAdministrator()
        {
            //Arrange
            _user.AddRole(Constants.UserAdministratorsRole);

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task ShouldNotAllow_ProductDelete_WhenNotAdministrator()
        {
            //Arrange

            //Act
            var allowed = await _authorizationService.AuthorizeAsync(_user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
    }
}
