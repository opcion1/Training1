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
using Training1.Services.Interfaces;
using Training1.Tests.Mock;
using Training1.Tests.Mock.Services;
using Xunit;

namespace Training1.Tests.Controllers
{
    public class SesshinControllerTest
    {
        private const int _testSesshinId = 1;
        private readonly Sesshin _testSesshin;
        private readonly MockSesshinService _mockService;
        private readonly SesshinsController _controller;

        public SesshinControllerTest()
        {
            _testSesshin = new Sesshin
            {
                Id = _testSesshinId,
                Name = "Winter sesshin",
                Description = "Night sesshin in Yujio Nyusanji",
                StartDate = new DateTime(2019, 12, 27),
                EndDate = new DateTime(2020, 01, 01),
                AppUserId = "userId"
            };
            _mockService = new MockSesshinService();
            _controller = GetSesshinsController(_mockService);
        }

        [Fact]
        public async Task _testSesshinIndexAsync()
        {
            // Arrange
            var sesshins = new Sesshin[] {
                new Sesshin { Id = 1, Name = "Winter sesshin", Description = "Night sesshin in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"},
                new Sesshin { Id = 2, Name = "Autumn sesshin", Description = "Autumn sesshin in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"},
                new Sesshin { Id = 3, Name = "End of Winter sesshin", Description = "End of Winter sesshin in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"},
                new Sesshin { Id = 4, Name = "Summertime sesshin", Description = "Summertime sesshin in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"},
                new Sesshin { Id = 5, Name = "Summer camp", Description = "Ango in Yujio Nyusanji", StartDate = new DateTime(2019, 12, 27), EndDate = new DateTime(2020, 01, 01), AppUserId = "userId"}
            };
            _mockService.MockListAsync(sesshins);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Sesshin>>(viewResult.ViewData.Model);
            Assert.Equal(5, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange

            // Act
            var result = await _controller.Details(null, null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testSesshinId, null);

            // Act
            var result = await _controller.Details(_testSesshinId, null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsSesshinView()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testSesshinId, _testSesshin);

            // Act
            var result = await _controller.Details(_testSesshinId, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Sesshin>(viewResult.ViewData.Model);

            Assert.Equal(_testSesshinId, model.Id);
            Assert.Equal("Winter sesshin", model.Name);
            Assert.Equal("Night sesshin in Yujio Nyusanji", model.Description);
            Assert.Equal(new DateTime(2019, 12, 27), model.StartDate);
            Assert.Equal(new DateTime(2020, 1, 1), model.EndDate);
            Assert.Equal("userId", model.AppUserId);
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndAddsSesshin_WhenModelStateIsValid()
        {
            // Arrange
            _mockService.MockCreateAsync(_testSesshin);

            // Act
            var result = await _controller.Create(_testSesshin);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockService.Verify();
        }

        [Fact]
        public async Task CreatePost_ReturnsViewResult_GivenInvalidModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _controller.Create(sesshin: null);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange

            // Act
            var result = await _controller.Edit(null, fromDetail: null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testSesshinId, null);

            // Act
            var result = await _controller.Edit(_testSesshinId, fromDetail: null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsSesshinView()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testSesshinId, _testSesshin);

            // Act
            var result = await _controller.Edit(_testSesshinId, fromDetail: null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Sesshin>(viewResult.ViewData.Model);

            Assert.Equal(_testSesshinId, model.Id);
            Assert.Equal("Winter sesshin", model.Name);
            Assert.Equal("Night sesshin in Yujio Nyusanji", model.Description);
            Assert.Equal(new DateTime(2019, 12, 27), model.StartDate);
            Assert.Equal(new DateTime(2020, 1, 1), model.EndDate);
            Assert.Equal("userId", model.AppUserId);
        }

        [Fact]
        public async Task EditPost_ReturnsHttpNotFound_ForIdDifferentSesshinId()
        {
            // Arrange
            int invalidSesshinId = _testSesshinId + 1;

            // Act
            var result = await _controller.Edit(invalidSesshinId, _testSesshin, false);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        [Fact]
        public async Task EditPost_ReturnsViewResult_GivenInvalidModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _controller.Edit(_testSesshinId, _testSesshin, false);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditPost_ReturnsARedirectAndAddsSesshin_WhenModelStateIsValid()
        {
            // Arrange
            _mockService.MockEditAsync(_testSesshin);

            // Act
            var result = await _controller.Edit(_testSesshinId, sesshin: _testSesshin, fromDetail: false);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockService.Verify();
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange

            // Act
            var result = await _controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testSesshinId, null);

            // Act
            var result = await _controller.Delete(_testSesshinId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsSesshinView()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testSesshinId, _testSesshin);

            // Act
            var result = await _controller.Delete(_testSesshinId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Sesshin>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task DeletePost_ReturnsARedirectAndDeleteSesshin()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testSesshinId, _testSesshin)
                .MockDeleteAsync(_testSesshinId);

            // Act
            var result = await _controller.DeleteConfirmed(_testSesshinId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockService.Verify();
        }


        private SesshinsController GetSesshinsController(MockSesshinService mockSesshin)
        {
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<ISesshinService>(sp => mockSesshin.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new SesshinsController(mockSesshin.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            return controller;
        }
    }
}
