
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Security.Claims;

namespace Training1.Tests.Mock
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
    }
}
