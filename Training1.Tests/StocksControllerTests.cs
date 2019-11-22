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
    public class StockControllerTest
    {
        [Fact]
        public async Task TestStockIndexAsync()
        {
            // Arrange
            var product = new Product() { Id = 123, Name = "Carot", Description = "Carot description", Category = ProductCategory.Vegetable };
            var stocks = new Stock[] {
                new Stock { StockId = 1, Quantity = 1.5m, PricePorUnity = 10, Currency = Currency.US_Dollar, CommandDate = DateTime.Today,  UnityType = UnityType.Kilogrammes, Product = product, ProductId = 123 },
                new Stock { StockId = 2, Quantity = 10m, PricePorUnity = 9, Currency = Currency.US_Dollar, CommandDate = DateTime.Today,  UnityType = UnityType.Kilogrammes, Product = product, ProductId = 123 },
                new Stock { StockId = 3, Quantity = 15.5m, PricePorUnity = 9.5m, Currency = Currency.US_Dollar, CommandDate = DateTime.Today,  UnityType = UnityType.Kilogrammes, Product = product, ProductId = 123 },
                new Stock { StockId = 4, Quantity = 4.5m, PricePorUnity = 8.75m, Currency = Currency.US_Dollar, CommandDate = DateTime.Today,  UnityType = UnityType.Kilogrammes, Product = product, ProductId = 123 }
            };

            Mock<IStockRepository> mockRepo = new MockStockRepository().MockListAsync(stocks);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IStockRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new StocksController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            var result = await controller.Index(null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Stock>>(viewResult.ViewData.Model);

            Assert.Equal(4, model.Count());
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndAddsStock_WhenModelStateIsValid()
        {
            // Arrange
            var product = new Product() { Id = 123, 
                                            Name = "Carot",
                                            Description = "Carot description", 
                                            Category = ProductCategory.Vegetable };
            var newStock = new Stock { Quantity = 1.5m, 
                                        PricePorUnity = 10, 
                                        Currency = Currency.US_Dollar, 
                                        CommandDate = DateTime.Today, 
                                        UnityType = UnityType.Kilogrammes, 
                                        Product = product, 
                                        ProductId = 123 };

            var mockRepo = new MockStockRepository().MockAddAsync(newStock);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IStockRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new StocksController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.Create(newStock);

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
            var mockRepo = new MockStockRepository();
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IStockRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new StocksController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Create(stock: null);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange
            var controller = new StocksController(stockRepository: null, authorizationService: null);

            // Act
            var result = await controller.Edit(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            int nonExistentStockId = 123;
            var mockRepo = new MockStockRepository();
            var controller = new StocksController(mockRepo.Object, authorizationService: null);

            // Act
            var result = await controller.Edit(nonExistentStockId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsStockView()
        {
            // Arrange
            int testSessionId = 123;
            var product = new Product()
            {
                Id = 123,
                Name = "Carot",
                Description = "Carot description",
                Category = ProductCategory.Vegetable
            };
            var newStock = new Stock
            {
                StockId = 123,
                Quantity = 1.5m,
                PricePorUnity = 10,
                Currency = Currency.US_Dollar,
                CommandDate = DateTime.Today,
                UnityType = UnityType.Kilogrammes,
                Product = product,
                ProductId = 123
            };
            var mockRepo = new MockStockRepository().MockGetByIdAsync(testSessionId, newStock);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IStockRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new StocksController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.Edit(testSessionId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Stock>(viewResult.ViewData.Model);

            Assert.Equal(123, model.StockId);
            Assert.Equal(10, model.PricePorUnity);
            Assert.Equal(Currency.US_Dollar, model.Currency);
            Assert.Equal(DateTime.Today, model.CommandDate);
            Assert.Equal(UnityType.Kilogrammes, model.UnityType);
            Assert.Equal(123, model.ProductId);
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForNulldId()
        {
            // Arrange
            var controller = new StocksController(stockRepository: null, authorizationService: null);

            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            int nonExistentStockId = 123;
            var mockRepo = new MockStockRepository().MockGetByIdAsync(nonExistentStockId, null);
            var controller = new StocksController(mockRepo.Object, authorizationService: null);

            // Act
            var result = await controller.Delete(nonExistentStockId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsStockView()
        {
            // Arrange
            int testSessionId = 123;
            var product = new Product()
            {
                Id = 123,
                Name = "Carot",
                Description = "Carot description",
                Category = ProductCategory.Vegetable
            };
            var stock = new Stock
            {
                StockId = 123,
                Quantity = 1.5m,
                PricePorUnity = 10,
                Currency = Currency.US_Dollar,
                CommandDate = DateTime.Today,
                UnityType = UnityType.Kilogrammes,
                Product = product,
                ProductId = 123
            };
            var mockRepo = new MockStockRepository().MockGetByIdAsync(testSessionId, stock);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IStockRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new StocksController(mockRepo.Object, authService);
            MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);

            // Act
            var result = await controller.Delete(testSessionId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Stock>(viewResult.ViewData.Model);

            Assert.Equal(123, model.StockId);
            Assert.Equal(10, model.PricePorUnity);
            Assert.Equal(Currency.US_Dollar, model.Currency);
            Assert.Equal(DateTime.Today, model.CommandDate);
            Assert.Equal(UnityType.Kilogrammes, model.UnityType);
            Assert.Equal(123, model.ProductId);
        }

        [Fact]
        public async Task DeletePost_ReturnsARedirectAndDeleteStock()
        {
            // Arrange
            int testSessionId = 123;
            var product = new Product()
            {
                Id = 123,
                Name = "Carot",
                Description = "Carot description",
                Category = ProductCategory.Vegetable
            };
            var stock = new Stock
            {
                StockId = 123,
                Quantity = 1.5m,
                PricePorUnity = 10,
                Currency = Currency.US_Dollar,
                CommandDate = DateTime.Today,
                UnityType = UnityType.Kilogrammes,
                Product = product,
                ProductId = 123
            };
            var mockRepo = new MockStockRepository()
                .MockGetByIdAsync(testSessionId, stock)
                .MockDeleteAsync(testSessionId);
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IStockRepository>(sp => mockRepo.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new StocksController(mockRepo.Object, authService);
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
