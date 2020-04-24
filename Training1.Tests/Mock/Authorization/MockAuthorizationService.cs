
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;

namespace Training1.Tests.Mock.Authorization
{

    public class MockAuthorizationService
    {
        public static IAuthorizationService BuildAuthorizationService(Action<IServiceCollection> setupServices = null)
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            services.AddAuthorization();
            services.AddOptions();
            services.AddLogging();
            setupServices?.Invoke(services);
            return services.BuildServiceProvider().GetRequiredService<IAuthorizationService>();
        }
        public static void SetupUserWithRole(Controller controller, string role)
        {
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson"),
                        new Claim(ClaimTypes.Role, role) }));

            var mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(hc => hc.User).Returns(user);
            mockContext.SetupGet(hc => hc.User.Identity.Name).Returns(user.Identity.Name);
            mockContext.Setup(hc => hc.User.IsInRole(role)).Returns(true);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = mockContext.Object
            };
        }


        //public static class DelegateFactory
        //{
        //    public static Func<ClaimsPrincipal, object, IAuthorizationRequirement, Task<AuthorizationResult>> AuthorizeAsync =
        //        (c, o, s) =>
        //        {
        //            return AuthorizationServiceExtensions.AuthorizeAsync(null, null, "");
        //        };
        //}

        //public Mock<IAuthorizationService> AuthorizationServiceMockResultFailed()
        //{
        //    var mockRepository = new Moq.MockRepository(Moq.MockBehavior.Strict);
        //    var mockFactory = mockRepository.Create<IAuthorizationService>();
        //    var ClaimsPrincipal = mockRepository.Create<ClaimsPrincipal>();
        //    mockFactory.Setup(x => x.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AppUser>(), It.IsAny<OperationAuthorizationRequirement>())).ReturnsAsync(AuthorizationResult.Failed);
        //    return mockFactory;
        //}
    }
}
