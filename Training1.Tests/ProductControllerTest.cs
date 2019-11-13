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
    public class ProductControllerTest
    {
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

            Mock<IProductRepository> mockRepo = new MockProductRepository().MockListAsync(products);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IProductRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new ProductsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            var result = await controller.Index(null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.ViewData.Model);

            Assert.Equal(4, model.Count());
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndAddsProduct_WhenModelStateIsValid()
        {
            // Arrange
            var newProduct = new Product()
            {
                Name = "Riz",
                Description = "Very popular in China",
                Category = ProductCategory.Cereal
            };

            var mockRepo = new MockProductRepository().MockAddAsync(newProduct);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IProductRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new ProductsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.Create(newProduct);

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
            var mockRepo = new MockProductRepository();
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IProductRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new ProductsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Create(product: null);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange
            var controller = new ProductsController(productRepository: null, authorizationService: null);

            // Act
            var result = await controller.Edit(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            int nonExistentProductId = 123;
            var mockRepo = new MockProductRepository();
            var controller = new ProductsController(mockRepo.Object, authorizationService: null);

            // Act
            var result = await controller.Edit(nonExistentProductId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsProductView()
        {
            // Arrange
            int testSessionId = 123;
            var newProduct = new Product() { Id = 123, Name = "Carot", Description = "Carot description", Category = ProductCategory.Vegetable };
            var mockRepo = new MockProductRepository().MockGetByIdAsync(testSessionId, newProduct);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IProductRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new ProductsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.Edit(testSessionId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);

            Assert.Equal(123, model.Id);
            Assert.Equal("Carot", model.Name);
            Assert.Equal("Carot description", model.Description);
            Assert.Equal(ProductCategory.Vegetable, model.Category);
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange
            var controller = new ProductsController(productRepository: null, authorizationService:null);

            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            int nonExistentProductId = 123;
            var mockRepo = new MockProductRepository().MockGetByIdAsync(nonExistentProductId, null);
            var controller = new ProductsController(mockRepo.Object, authorizationService:null);

            // Act
            var result = await controller.Delete(nonExistentProductId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsProductView()
        {
            // Arrange
            int testSessionId = 123;
            var product = new Product() { Id = 123, Name = "Carot", Description = "Carot description", Category = ProductCategory.Vegetable };
            var mockRepo = new MockProductRepository().MockGetByIdAsync(testSessionId, product);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IProductRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new ProductsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.Delete(testSessionId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);

            Assert.Equal(123, model.Id);
            Assert.Equal("Carot", model.Name);
            Assert.Equal("Carot description", model.Description);
            Assert.Equal(ProductCategory.Vegetable, model.Category);
        }

        [Fact]
        public async Task DeletePost_ReturnsARedirectAndDeleteProduct()
        {
            // Arrange
            int testSessionId = 123;
            var product = new Product() { Id = 123, Name = "Carot", Description = "Carot description", Category = ProductCategory.Vegetable };
            var mockRepo = new MockProductRepository()
                .MockGetByIdAsync(testSessionId, product)
                .MockDeleteAsync(testSessionId);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IProductRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new ProductsController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.DeleteConfirmed(testSessionId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
    }
}
