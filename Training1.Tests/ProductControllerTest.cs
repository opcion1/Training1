using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Training1.Authorization;
//using System.Web.Mvc;
using Training1.Controllers;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Repositories;
using Training1.Tests.Mock;
using Xunit;

namespace Training1.Tests
{
    public class ProductControllerTest
    {
        private const int _testProductId = 1;
        private readonly Product _testProduct;
        private readonly MockProductRepository _mockRepo;
        private readonly ProductsController _controller;
        private readonly MockConfiguration _mockConfig;

        public ProductControllerTest()
        {
            _testProduct = new Product() { Id = _testProductId, Name = "Carot", Category = ProductCategory.Vegetable };
            _mockRepo = new MockProductRepository();
            _mockConfig = new MockConfiguration();
            _controller = GetProductsController(_mockRepo, _mockConfig);
        }

        [Fact]
        public async Task TestProductIndexAsync()
        {
            // Arrange
            var products = new Product[] { 
                new Product { Id = 1, Name = "Carots", Category = ProductCategory.Vegetable },
                new Product { Id = 2, Name = "Potatoes", Category = ProductCategory.Vegetable },
                new Product { Id = 3, Name = "Ketchup", Category = ProductCategory.Condiment },
                new Product { Id = 4, Name = "Apple", Category = ProductCategory.Fruit }
            };
            _mockRepo.MockListAsync(products);
            _mockConfig.MockGetValueInt("ItemsPerPage");

            //Act
            var result = await _controller.Index(null, null, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductsViewModel>(viewResult.ViewData.Model);

            Assert.Equal(4, model.Products.Count());
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndAddsProduct_WhenModelStateIsValid()
        {
            // Arrange
            _mockRepo.MockAddAsync(_testProduct);

            // Act
            var result = await _controller.Create(_testProduct);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepo.Verify();
        }

        [Fact]
        public async Task CreatePost_ReturnsViewResult_GivenInvalidModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _controller.Create(product: null);

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
            _mockRepo.MockGetByIdAsync(_testProductId, null);

            // Act
            var result = await _controller.Details(_testProductId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsProductView()
        {
            // Arrange
            _mockRepo.MockGetByIdAsync(_testProductId, _testProduct);

            // Act
            var result = await _controller.Details(_testProductId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);

            Assert.Equal(_testProductId, model.Id);
            Assert.Equal("Carot", model.Name);
            Assert.Equal(ProductCategory.Vegetable, model.Category);
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
            _mockRepo.MockGetByIdAsync(_testProductId, null);

            // Act
            var result = await _controller.Edit(_testProductId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsProductView()
        {
            // Arrange
            _mockRepo.MockGetByIdAsync(_testProductId, _testProduct);

            // Act
            var result = await _controller.Edit(_testProductId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);

            Assert.Equal(_testProductId, model.Id);
            Assert.Equal("Carot", model.Name);
            Assert.Equal(ProductCategory.Vegetable, model.Category);
        }


        [Fact]
        public async Task EditPost_ReturnsHttpNotFound_ForIdDifferentSesshinId()
        {
            // Arrange
            int invalidSesshinId = _testProductId + 1;

            // Act
            var result = await _controller.Edit(invalidSesshinId, _testProduct);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditPost_ReturnsViewResult_GivenInvalidModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _controller.Edit(_testProductId, _testProduct);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditPost_ReturnsARedirectAndAddsSesshin_WhenModelStateIsValid()
        {
            // Arrange
            _mockRepo.MockUpdateAsync(_testProduct);

            // Act
            var result = await _controller.Edit(_testProductId, _testProduct);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepo.Verify();
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
            _mockRepo.MockGetByIdAsync(_testProductId, null);

            // Act
            var result = await _controller.Delete(_testProductId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsProductView()
        {
            // Arrange
            _mockRepo.MockGetByIdAsync(_testProductId, _testProduct);

            // Act
            var result = await _controller.Delete(_testProductId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);

            Assert.Equal(_testProductId, model.Id);
            Assert.Equal("Carot", model.Name);
            Assert.Equal(ProductCategory.Vegetable, model.Category);
        }

        [Fact]
        public async Task DeletePost_ReturnsARedirectAndDeleteProduct()
        {
            // Arrange
            _mockRepo
                .MockGetByIdAsync(_testProductId, _testProduct)
                .MockDeleteAsync(_testProductId);

            // Act
            var result = await _controller.DeleteConfirmed(_testProductId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepo.Verify();
        }

        private ProductsController GetProductsController(MockProductRepository mockProductRepository, Mock<IConfiguration> mockConfiguration = null)
        {
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IProductRepository>(sp => mockProductRepository.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new ProductsController(mockProductRepository.Object, authService, configuration: mockConfiguration?.Object);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            return controller;
        }
    }
}
