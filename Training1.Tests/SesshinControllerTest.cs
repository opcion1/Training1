using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Training1.Authorization;
//using System.Web.Mvc;
using Training1.Controllers;
using Training1.Models;
using Training1.Repositories;
using Training1.Tests.Mock;
using Xunit;

namespace Training1.Tests
{
    public class SesshinControllerTest
    {
        [Fact]
        public async Task TestSesshinIndexAsync()
        {
            // Arrange
            var sesshins = new Sesshin[] {
                new Sesshin { SesshinId = 1, Name = "Winter sesshin", Description = "Night sesshin in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"},
                new Sesshin { SesshinId = 2, Name = "Autumn sesshin", Description = "Autumn sesshin in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"},
                new Sesshin { SesshinId = 3, Name = "End of Winter sesshin", Description = "End of Winter sesshin in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"},
                new Sesshin { SesshinId = 4, Name = "Summertime sesshin", Description = "Summertime sesshin in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"},
                new Sesshin { SesshinId = 5, Name = "Summer camp", Description = "Ango in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"}
        };

            Mock<ISesshinRepository> mockRepo = new MockSesshinRepository().MockListAsync(sesshins);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<ISesshinRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new SesshinsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Sesshin>>(viewResult.ViewData.Model);

            Assert.Equal(5, model.Count());
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndAddsSesshin_WhenModelStateIsValid()
        {
            // Arrange
            var newSesshin = new Sesshin
            {
                SesshinId = 123,
                Name = "Winter sesshin",
                Description = "Night sesshin in Yujio Nyusanji",
                StartDate = new DateTime(2019, 12, 27),
                EndDate = new DateTime(2020, 01, 01),
                AppUserId = "userId"
            };

            var mockRepo = new MockSesshinRepository().MockAddAsync(newSesshin);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<ISesshinRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new SesshinsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.Create(newSesshin);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }

        [Fact]
        public async Task Create_ReturnsViewResult_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new MockSesshinRepository();
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<ISesshinRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new SesshinsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Create(sesshin: null);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange
            var controller = new SesshinsController(sesshinRepository: null, authorizationService: null);

            // Act
            var result = await controller.Edit(null, null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            int nonExistentSesshinId = 123;
            var mockRepo = new MockSesshinRepository();
            var controller = new SesshinsController(mockRepo.Object, authorizationService: null);

            // Act
            var result = await controller.Edit(nonExistentSesshinId, null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsSesshinView()
        {
            // Arrange
            int testSesshinId = 123;
            var newSesshin = new Sesshin
            {
                SesshinId = testSesshinId,
                Name = "Winter sesshin",
                Description = "Night sesshin in Yujio Nyusanji",
                StartDate = new DateTime(2019, 12, 27),
                EndDate = new DateTime(2020, 01, 01),
                AppUserId = "userId"
            };
            var mockRepo = new MockSesshinRepository().MockGetByIdAsync(testSesshinId, newSesshin);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<ISesshinRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new SesshinsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.Edit(testSesshinId, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Sesshin>(viewResult.ViewData.Model);

            Assert.Equal(123, model.SesshinId);
            Assert.Equal("Winter sesshin", model.Name);
            Assert.Equal("Night sesshin in Yujio Nyusanji", model.Description);
            Assert.Equal(new DateTime(2019, 12, 27), model.StartDate);
            Assert.Equal(new DateTime(2020, 1, 1), model.EndDate);
            Assert.Equal("userId", model.AppUserId);
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange
            var controller = new SesshinsController(sesshinRepository: null, authorizationService: null);

            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            int nonExistentSesshinId = 123;
            var mockRepo = new MockSesshinRepository().MockGetByIdAsync(nonExistentSesshinId, null);
            var controller = new SesshinsController(mockRepo.Object, authorizationService: null);

            // Act
            var result = await controller.Delete(nonExistentSesshinId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsSesshinView()
        {
            // Arrange
            int testSesshinId = 123;
            var sesshin = new Sesshin
            {
                SesshinId = 123,
                Name = "Winter sesshin",
                Description = "Night sesshin in Yujio Nyusanji",
                StartDate = new DateTime(2019, 12, 27),
                EndDate = new DateTime(2020, 01, 01),
                AppUserId = "userId"
            };
            var mockRepo = new MockSesshinRepository().MockGetByIdAsync(testSesshinId, sesshin);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<ISesshinRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new SesshinsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.Delete(testSesshinId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Sesshin>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task DeletePost_ReturnsARedirectAndDeleteSesshin()
        {
            // Arrange
            var sesshin = new Sesshin
            {
                SesshinId = 123,
                Name = "Winter sesshin",
                Description = "Night sesshin in Yujio Nyusanji",
                StartDate = new DateTime(2019, 12, 27),
                EndDate = new DateTime(2020, 01, 01),
                AppUserId = "userId"
            };
            var mockRepo = new MockSesshinRepository()
                .MockGetByIdAsync(123, sesshin)
                .MockDeleteAsync(123);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<ISesshinRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new SesshinsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.DeleteConfirmed(123);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
    }
}
