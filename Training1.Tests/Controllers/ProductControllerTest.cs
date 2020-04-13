using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Training1.Authorization;
//using System.Web.Mvc;
using Training1.Controllers;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Repositories;
using Training1.Services.Interfaces;
using Training1.Tests.Mock;
using Training1.Tests.Mock.Services;
using Xunit;

namespace Training1.Tests
{
    public class ProductControllerTest
    {
        private const int _testProductId = 1;
        private readonly Product _testProduct;
        private readonly MockProductService _mockService;
        private readonly ProductsController _controller;

        public ProductControllerTest()
        {
            _testProduct = new Product() { Id = _testProductId, Name = "Carot", Category = ProductCategory.Vegetable };
            _mockService = new MockProductService();
            _controller = GetProductsController(_mockService);
        }

        [Fact]
        public async Task TestProductIndexAsync()
        {
            // Arrange
            var products = new Product[] { 
                _testProduct
            };
            _mockService.MockSearchSortAndPageProductAll(new SearchSortPageResult<Product> { Entities = products, ItemsPerPage = 10, TotalItems = 4 });

            //Act
            var result = await _controller.Index(null, null, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductsViewModel>(viewResult.ViewData.Model);
            _mockService.VerifySearchSortAndPageProductAll(Times.Once);
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndAddsProduct_WhenModelStateIsValid()
        {
            // Arrange
            _mockService.MockCreateAsync(_testProduct);

            // Act
            var result = await _controller.Create(_testProduct);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockService.Verify();
            _mockService.VerifyCreateAsync(Times.Once);
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
            _mockService.MockGetByIdAsync(_testProductId, null);

            // Act
            var result = await _controller.Details(_testProductId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockService.VerifyGetByIdAsync(Times.Once);
        }

        [Fact]
        public async Task Details_ReturnsProductView()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testProductId, _testProduct);

            // Act
            var result = await _controller.Details(_testProductId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
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
            _mockService.MockGetByIdAsync(_testProductId, null);

            // Act
            var result = await _controller.Edit(_testProductId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsProductView()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testProductId, _testProduct);

            // Act
            var result = await _controller.Edit(_testProductId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
            _mockService.VerifyGetByIdAsync(Times.Once);
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
            _mockService.MockEditAsync(_testProduct);

            // Act
            var result = await _controller.Edit(_testProductId, _testProduct);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
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
            _mockService.MockGetByIdAsync(_testProductId, null);

            // Act
            var result = await _controller.Delete(_testProductId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsProductView()
        {
            // Arrange
            _mockService.MockGetByIdAsync(_testProductId, _testProduct);

            // Act
            var result = await _controller.Delete(_testProductId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task DeletePost_ReturnsARedirectAndDeleteProduct()
        {
            // Arrange
            _mockService
                .MockGetByIdAsync(_testProductId, _testProduct)
                .MockDeleteAsync(_testProductId);

            // Act
            var result = await _controller.DeleteConfirmed(_testProductId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockService.Verify();
            _mockService.VerifyDeleteAsync(Times.Once);
        }

        private ProductsController GetProductsController(MockProductService mockProductService)
        {
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IProductService>(sp => mockProductService.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new ProductsController(mockProductService.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            return controller;
        }
    }
}
