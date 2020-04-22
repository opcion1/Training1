using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Controllers;
using Training1.Services.Interfaces;
using Training1.Tests.Mock;
using Training1.Tests.Mock.Services;
using Xunit;

namespace Training1.Tests.Controllers
{
    public class HomeControllerTest
    {
        private readonly MockHomeService _mockHomeService;
        private readonly HomeController _homeController;

        public HomeControllerTest()
        {
            _mockHomeService = new MockHomeService();
            _homeController = GetHomeController(_mockHomeService);
        }

        [Fact]
        public async Task UpdateAppStyle_ReturnNotFoundWhenKeyNotFound()
        {
            // Arrange
            _mockHomeService.MockUpdateAppStyleAndThrowKeyNotFoundException();

            //Act
            var result = await _homeController.UpdateAppStyle(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateAppStyle_ReturnOk()
        {
            // Arrange
            _mockHomeService.MockUpdateAppStyle();

            //Act
            var result = await _homeController.UpdateAppStyle(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.IsType<OkResult>(result);
            _mockHomeService.VerifyUpdateAppStyle(Times.Once);
        }

        private HomeController GetHomeController(MockHomeService mockHomeService)
        {
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IHomeService>(sp => mockHomeService.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new HomeController(mockHomeService.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            return controller;
        }
    }
}
