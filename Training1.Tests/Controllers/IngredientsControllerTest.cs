using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Training1.Controllers;
using Training1.Models;
using Training1.Tests.Mock.Services;
using Xunit;

namespace Training1.Tests.Controllers
{
    public class IngredientsControllerTest
    {
        private readonly MockIngredientService _mockService;
        private readonly IngredientsController _controller;
        private readonly Ingredient _testIngredient;
        private const int _idTestIngredient = 1;

        public IngredientsControllerTest()
        {
            _mockService = new MockIngredientService();
            _controller = GetIngredientsController(_mockService);
            _testIngredient = new Ingredient { Id = _idTestIngredient, FoodId = 1, ProductId = 1, Quantity = 1, UnityType = UnityType.Kilogrammes };
        }

        [Fact]
        public async Task Create_ReturnViewResult()
        {
            //Arrange
            _mockService
                .MockCreateAsync()
                .MockGetByIdAsync(new Ingredient());

            //Act
            var result = await _controller.Create(_testIngredient);

            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Ingredient>(viewResult.Value);
            _mockService.Verify();
        }

        [Fact]
        public async Task Edit_ReturnNotFound_WhenDbUpdateConcurrencyException()
        {
            //Arrange
            _mockService.MockEditAsync_ThrowsDbUpdateConcurrencyException();

            //Act
            var result = await _controller.Edit(_testIngredient);

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
            _mockService.Verify();
        }

        [Fact]
        public async Task Edit_ReturnViewResult()
        {
            //Arrange
            _mockService
                .MockEditAsync()
                .MockGetByIdAsync(new Ingredient());

            //Act
            var result = await _controller.Edit(_testIngredient);

            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Ingredient>(viewResult.Value);
            _mockService.Verify();
        }

        [Fact]
        public async Task Delete_ReturnOk()
        {
            //Arrange
            _mockService.MockDeleteAsync();

            //Act
            var result = await _controller.Delete(_idTestIngredient);

            //Assert
            var viewResult = Assert.IsType<OkResult>(result);
            _mockService.Verify();
        }

        [Fact]
        public async Task Details_ReturnNotFound_WhenIdIsNull()
        {
            //Arrange

            //Act
            var result = await _controller.Details(null);

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnNotFound_WhenIngredientIsNull()
        {
            //Arrange
            _mockService.MockGetByIdAsync(null);

            //Act
            var result = await _controller.Details(_idTestIngredient);

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
            _mockService.Verify();
        }

        [Fact]
        public async Task Details_ReturnOk()
        {
            //Arrange
            _mockService.MockGetByIdAsync(_testIngredient);

            //Act
            var result = await _controller.Details(_idTestIngredient);

            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            _mockService.Verify();
        }


        private IngredientsController GetIngredientsController(MockIngredientService mockService)
        {

            var controller = new IngredientsController(mockService.Object);

            return controller;
        }
    }
}
