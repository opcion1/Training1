
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Models;
using Training1.Tests.Mock;
using Xunit;

namespace Training1.Tests
{
    public class StockAuthorizationServiceTests
    {
        [Fact]
        public async Task AdminAuthorization_ShowAllowStockCreateWhenAdministrator()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAdministratorsRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowAllowStockCreateWhenChef()
        {
            //Arrange
            var userManager = MockIdentity.MockUserManager<AppUser>().Object;
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler>(sp => new ChefAuthorizationHandler(userManager));
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserChefRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task AccountantAuthorization_ShowAllowStockCreateWhenAccountant()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AccountantAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAccountantRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }


        [Fact]
        public async Task AdminAuthorization_ShowAllowStockUpdateWhenAdministrator()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAdministratorsRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowAllowStockUpdateWhenChef()
        {
            //Arrange
            var userManager = MockIdentity.MockUserManager<AppUser>().Object;
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler>(sp => new ChefAuthorizationHandler(userManager));
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserChefRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task AccountantAuthorization_ShowAllowStockUpdateWhenAccountant()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AccountantAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAccountantRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }


        [Fact]
        public async Task AdminAuthorization_ShowAllowStockReadWhenAdministrator()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAdministratorsRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowAllowStockReadWhenChef()
        {
            //Arrange
            var userManager = MockIdentity.MockUserManager<AppUser>().Object;
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler>(sp => new ChefAuthorizationHandler(userManager));
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserChefRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task AccountantAuthorization_ShowAllowStockReadWhenAccountant()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AccountantAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAccountantRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }



        [Fact]
        public async Task AdminAuthorization_ShowAllowStockDeleteWhenAdministrator()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAdministratorsRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Delete);

            // Assert
            Assert.True(allowed.Succeeded);
        }
        [Fact]
        public async Task AdminAuthorization_ShowNotAllowStockDeleteWhenChef()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserChefRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task AdminAuthorization_ShowNotAllowStockDeleteWhenAccountant()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAccountantRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ChefAuthorization_ShowNotAllowStockDeleteWhenChef()
        {
            //Arrange
            var userManager = MockIdentity.MockUserManager<AppUser>().Object;
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler>(sp => new ChefAuthorizationHandler(userManager));
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserChefRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowNotAllowStockDeleteWhenAdmin()
        {
            //Arrange
            var userManager = MockIdentity.MockUserManager<AppUser>().Object;
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler>(sp => new ChefAuthorizationHandler(userManager));
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAdministratorsRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowNotAllowStockDeleteWhenAccountant()
        {
            //Arrange
            var userManager = MockIdentity.MockUserManager<AppUser>().Object;
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler>(sp => new ChefAuthorizationHandler(userManager));
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAccountantRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task AccountantAuthorization_ShowNotAllowStockDeleteWhenChef()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AccountantAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserChefRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task AccountantAuthorization_ShowNotAllowStockDeleteWhenAdmin()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AccountantAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAdministratorsRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task AccountantAuthorization_ShowNotAllowStockDeleteWhenAccountant()
        {
            //Arrange
            var authorizationService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IAuthorizationHandler, AccountantAuthorizationHandler>();
            });
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, Constants.UserAccountantRole) }));

            //Act
            var allowed = await authorizationService.AuthorizeAsync(user, new Stock(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
    }
}
