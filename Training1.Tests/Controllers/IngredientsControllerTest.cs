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


        private IngredientsController GetIngredientsController(MockIngredientService mockService)
        {

            var controller = new IngredientsController(mockService.Object);

            return controller;
        }
    }
}
