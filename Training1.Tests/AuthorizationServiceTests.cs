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
    public class AuthorizationServiceTests
    {
        [Fact]
        public async Task AdminAuthorization_ShowAllowProductCreateWhenAdministrator()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowAllowProductCreateWhenChef()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task AccountantAuthorization_ShowAllowProductCreateWhenAccountant()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Create);

            // Assert
            Assert.True(allowed.Succeeded);
        }


        [Fact]
        public async Task AdminAuthorization_ShowAllowProductUpdateWhenAdministrator()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowAllowProductUpdateWhenChef()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task AccountantAuthorization_ShowAllowProductUpdateWhenAccountant()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Update);

            // Assert
            Assert.True(allowed.Succeeded);
        }


        [Fact]
        public async Task AdminAuthorization_ShowAllowProductReadWhenAdministrator()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowAllowProductReadWhenChef()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }

        [Fact]
        public async Task AccountantAuthorization_ShowAllowProductReadWhenAccountant()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Read);

            // Assert
            Assert.True(allowed.Succeeded);
        }



        [Fact]
        public async Task AdminAuthorization_ShowAllowProductDeleteWhenAdministrator()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Delete);

            // Assert
            Assert.True(allowed.Succeeded);
        }
        [Fact]
        public async Task AdminAuthorization_ShowNotAllowProductDeleteWhenChef()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task AdminAuthorization_ShowNotAllowProductDeleteWhenAccountant()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task ChefAuthorization_ShowNotAllowProductDeleteWhenChef()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowNotAllowProductDeleteWhenAdmin()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task ChefAuthorization_ShowNotAllowProductDeleteWhenAccountant()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        [Fact]
        public async Task AccountantAuthorization_ShowNotAllowProductDeleteWhenChef()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task AccountantAuthorization_ShowNotAllowProductDeleteWhenAdmin()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
        [Fact]
        public async Task AccountantAuthorization_ShowNotAllowProductDeleteWhenAccountant()
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
            var allowed = await authorizationService.AuthorizeAsync(user, new Product(), UserOperations.Delete);

            // Assert
            Assert.False(allowed.Succeeded);
        }
    }
}
