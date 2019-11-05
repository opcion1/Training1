using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Training1.Controllers;
using Training1.Models;
using Training1.Repositories;
using Xunit;

namespace Training1.Tests
{
    public class ProductControllerTest
    {
        [Fact]
        public async Task TestProductIndexAsync()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.ListAsync()).ReturnsAsync((new Product[] { 
                new Product { Id = 1, Name = "Carots", Category = ProductCategory.Vegetable },
                new Product { Id = 2, Name = "Potatoes", Category = ProductCategory.Vegetable },
                new Product { Id = 3, Name = "Ketchup", Category = ProductCategory.Condiment },
                new Product { Id = 4, Name = "Apple", Category = ProductCategory.Fruit }
            }).Cast<Product>().ToList());

            var controller = new ProductsController(mock.Object);

            var result = await controller.Index(null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.ViewData.Model);

            Assert.Equal(4, model.Count());
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndAddsProduct_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new ProductsController(mockRepo.Object);
            var newProduct = new Product()
            {
                Name = "Riz",
                Description ="Very popular in China",
                Category = ProductCategory.Cereal
            };

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
            var mockRepo = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepo.Object);
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
            var controller = new ProductsController(productRepository: null);

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
            var mockRepo = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepo.Object);

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
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync(new Product() { Id=123,Name="Carot", Description = "Carot description",Category=ProductCategory.Vegetable});
            var controller = new ProductsController(mockRepo.Object);

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
            var controller = new ProductsController(productRepository: null);

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
            var mockRepo = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepo.Object);

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
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync(new Product() { Id = 123, Name = "Carot", Description = "Carot description", Category = ProductCategory.Vegetable });
            var controller = new ProductsController(mockRepo.Object);

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
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(new Product() { Id = 1, Name = "Carot", Description = "Carot description", Category = ProductCategory.Vegetable });
            mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new ProductsController(mockRepo.Object);

            // Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
    }
}
