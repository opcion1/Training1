using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Authorization;
using Training1.Controllers;
using Training1.Models;
using Training1.Services.Interfaces;
using Training1.Tests.Mock;
using Training1.Tests.Mock.Repositories;
using Training1.Tests.Mock.Services;
using Xunit;

namespace Training1.Tests.Controllers
{
    public class StockControllerTest
    {
        private const int _testStockId = 1;
        private const int _testProductId = 1;
        private readonly Stock _testStock;
        private readonly MockStockService _mockService;
        private readonly StocksController _controller;

        public StockControllerTest()
        {
            _testStock = new Stock() { Id = _testStockId, ProductId = _testProductId, Quantity = 2.0M, Currency = Currency.Euro, PricePorUnity = 8, TotalPrice = 16};
            _mockService = new MockStockService();
            _controller = GetStocksController(_mockService);
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndAddsStock_WhenModelStateIsValid()
        {
            // Arrange
            _mockService.MockCreateAsync(_testStock);

            // Act
            var result = await _controller.Create(_testStock);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Products", redirectToActionResult.ControllerName);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            _mockService.Verify();
            _mockService.VerifyCreateAsync(Times.Once);
        }

        [Fact]
        public async Task CreatePost_ReturnsViewResult_GivenInvalidModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _controller.Create(stock: null);

            // Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task Details_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange

            // Act
            var result = await _controller.Details(id: null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testStockId, null);

            // Act
            var result = await _controller.Details(_testStockId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockService.VerifyGetByIdAsync(Times.Once);
        }

        [Fact]
        public async Task Details_ReturnsStockView()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testStockId, _testStock);

            // Act
            var result = await _controller.Details(_testStockId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Stock>(viewResult.ViewData.Model);
            _mockService.VerifyGetByIdAsync(Times.Once);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange

            // Act
            var result = await _controller.Edit(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testStockId, null);

            // Act
            var result = await _controller.Edit(_testStockId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsStockView()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testStockId, _testStock);

            // Act
            var result = await _controller.Edit(_testStockId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Stock>(viewResult.ViewData.Model);
            _mockService.VerifyGetByIdAsync(Times.Once);
        }

        [Fact]
        public async Task EditPost_ReturnsHttpNotFound_ForIdDifferentSesshinId()
        {
            // Arrange
            int invalidSesshinId = _testStockId + 1;

            // Act
            var result = await _controller.Edit(invalidSesshinId, _testStock);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditPost_ReturnsViewResult_GivenInvalidModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _controller.Edit(_testStockId, _testStock);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditPost_ReturnsARedirectAndAddsSesshin_WhenModelStateIsValid()
        {
            // Arrange
            _mockService.MockEditAsync(_testStock);

            // Act
            var result = await _controller.Edit(_testStockId, _testStock);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Products", redirectToActionResult.ControllerName);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            _mockService.Verify();
            _mockService.VerifyEditAsync(Times.Once);
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
            _mockService.MockGetByIdAsync(_testStockId, null);

            // Act
            var result = await _controller.Delete(_testStockId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsStockView()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testStockId, _testStock);

            // Act
            var result = await _controller.Delete(_testStockId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Stock>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task DeletePost_ReturnsARedirectAndDeleteStock()
        {
            // Arrange
            _mockService
                .MockGetByIdAsync(_testStockId, _testStock)
                .MockDeleteAsync(_testStockId);

            // Act
            var result = await _controller.DeleteConfirmed(_testStockId, _testProductId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Products", redirectToActionResult.ControllerName);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            _mockService.Verify();
            _mockService.VerifyDeleteAsync(Times.Once);
        }

        private StocksController GetStocksController(MockStockService mockStockService)
        {
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IStockService>(sp => mockStockService.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new StocksController(mockStockService.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            return controller;
        }
    }
}
