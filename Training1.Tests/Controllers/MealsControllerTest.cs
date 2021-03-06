﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Authorization;
using Training1.Controllers;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Services.Interfaces;
using Training1.Tests.Mock.Authorization;
using Training1.Tests.Mock.Services;
using Xunit;

namespace Training1.Tests.Controllers
{
    public class MealsControllerTest
    {
        private readonly MockMealService _mockService;
        private readonly MealsController _controller;
        private readonly MealsController _controllerWithNoRole;
        private readonly Food _testFood;
        private readonly MealFoodViewModel _testMealFoodViewModel;

        public MealsControllerTest()
        {
            _mockService = new MockMealService();
            _controller = GetMealsController(_mockService, true);
            _controllerWithNoRole = GetMealsController(_mockService, false);
            _testFood = new Food { Id = 1, Name = "Test Food" };
            _testMealFoodViewModel = new MealFoodViewModel { Food = _testFood, MealId = 1, SesshinId = 1 };
        }

        [Fact]
        public async Task AddFood_ReturnMealFoodViewModel()
        {
            //Arrange
            _mockService.MockGetMealFoodViewModel();

            //Act
            var result = await _controller.AddFood(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MealFoodViewModel>(viewResult.ViewData.Model);
            _mockService.Verify();
        }

        [Fact]
        public async Task AddFoodPost_ReturnMealFoodViewModel_WhenModelIsNotValid()
        {
            //Arrange
            _controller.ModelState.AddModelError("error", "some error");

            //Act
            var result = await _controller.AddFood(_testMealFoodViewModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MealFoodViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task AddFoodPost_ReturnChallengeResult_WhenFoodNotExistsAndNotAuthorizedToCreateFood()
        {
            //Arrange
            _mockService.MockExistsFood(false);

            //Act
            var result = await _controllerWithNoRole.AddFood(_testMealFoodViewModel);

            //Assert
            Assert.IsType<ChallengeResult>(result);
            _mockService.Verify();
        }

        [Fact]
        public async Task AddFoodPost_ReturnChallengeResult_WhenFoodExistsAndNotAuthorizedToCreateMealFood()
        {
            //Arrange
            _mockService.MockExistsFood(true);

            //Act
            var result = await _controllerWithNoRole.AddFood(_testMealFoodViewModel);

            //Assert
            Assert.IsType<ChallengeResult>(result);
            _mockService.Verify();
        }

        [Fact]
        public async Task AddFoodPost_RedirectToAction_WhenModelValid_FoodNotExists_And_AuthorizationsOk()
        {
            //Arrange
            _mockService
                .MockExistsFood(false)
                .MockCreateFoodAsync()
                .MockCreateMealFoodAsync();

            //Act
            var result = await _controller.AddFood(_testMealFoodViewModel);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Meals",redirectToActionResult.ControllerName);
            Assert.Equal("EditFood", redirectToActionResult.ActionName);
            _mockService.Verify();
        }

        [Fact]
        public async Task AddFoodPost_RedirectToAction_WhenModelValid_FoodExists_And_AuthorizationsOk()
        {
            //Arrange
            _mockService
                .MockExistsFood(true)
                .MockCreateMealFoodAsync();

            //Act
            var result = await _controller.AddFood(_testMealFoodViewModel);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Meals", redirectToActionResult.ControllerName);
            Assert.Equal("EditFood", redirectToActionResult.ActionName);
            _mockService.Verify();
        }

        [Fact]
        public async Task EditFood_ReturnMealFoodViewModel()
        {
            //Arrange
            _mockService.MockGetFoodByIdAsync(_testFood);

            //Act
            var result = await _controller.EditFood(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MealFoodViewModel>(viewResult.ViewData.Model);
            _mockService.Verify();
        }

        [Fact]
        public async Task EditFoodPost_ReturnMealFoodViewModel_WhenModelIsNotValid()
        {
            //Arrange
            _controller.ModelState.AddModelError("error", "some error");

            //Act
            var result = await _controller.EditFood(_testMealFoodViewModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MealFoodViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task EditFoodPost_ReturnNotFound_WhenFoodNotExists()
        {
            //Arrange
            _mockService.MockExistsFood(false);

            //Act
            var result = await _controllerWithNoRole.AddFood(_testMealFoodViewModel);

            //Assert
            Assert.IsType<ChallengeResult>(result);
            _mockService.Verify();
        }

        [Fact]
        public async Task EditFoodPost_ReturnChallengeResult_WhenFoodExistsAndNotAuthorizedToCreateMealFood()
        {
            //Arrange
            _mockService.MockExistsFood(true);

            //Act
            var result = await _controllerWithNoRole.EditFood(_testMealFoodViewModel);

            //Assert
            Assert.IsType<ChallengeResult>(result);
            _mockService.Verify();
        }

        [Fact]
        public async Task EditFoodPost_RedirectToAction_WhenModelValid_FoodExists_And_AuthorizationsOk()
        {
            //Arrange
            _mockService
                .MockExistsFood(true)
                .MockEditFoodAsync();

            //Act
            var result = await _controller.EditFood(_testMealFoodViewModel);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Sesshins", redirectToActionResult.ControllerName);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            _mockService.Verify();
        }
        [Fact]
        public async Task DeleteFoodPost_ReturnChallengeResult_When_NotAuthorizedToDeleteMealFood()
        {
            //Arrange

            //Act
            var result = await _controllerWithNoRole.DeleteMealFood(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            //Assert
            Assert.IsType<ChallengeResult>(result);
        }
        [Fact]
        public async Task DeleteFoodPost_RedirectToAction_When_AuthorizationsOk()
        {
            //Arrange
            _mockService.MockDeleteMealFoodAsync();

            //Act
            var result = await _controller.DeleteMealFood(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Sesshins", redirectToActionResult.ControllerName);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            _mockService.Verify();
        }

        [Fact]
        public async Task SearchFood_ReturnBadRequest_When_ThrowException()
        {
            //Arrange
            _mockService.MockSearchFoodByNameAsyncAndThorwsException();

            //Act
            var result = await _controller.SearchFood(It.IsAny<string>());

            //Assert
            Assert.IsType<BadRequestResult>(result);
            _mockService.Verify();
        }

        [Fact]
        public async Task SearchFood_ReturnOkObjectResult_WhenNoException()
        {
            //Arrange
            var foods = new List<Food> { _testFood };
            _mockService.MockSearchFoodByNameAsync(foods);

            //Act
            var result = await _controller.SearchFood(It.IsAny<string>());

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Food>>(objectResult.Value);
            _mockService.Verify();
        }

        private MealsController GetMealsController(MockMealService mockService, bool addRole)
        {
            var authService = MockAuthorizationService.BuildAuthorizationService(services =>
            {
                services.AddScoped<IMealService>(sp => mockService.Object);
                services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();
            });

            var controller = new MealsController(mockService.Object, authService);
            if (addRole)
            {
                MockAuthorizationService.SetupUserWithRole(controller, Constants.UserAdministratorsRole);
            }

            return controller;
        }
    }
}
