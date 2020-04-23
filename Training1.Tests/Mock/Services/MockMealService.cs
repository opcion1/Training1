using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Services.Interfaces;

namespace Training1.Tests.Mock.Services
{
    public class MockMealService : Mock<IMealService>
    {
        public MockMealService()
        {

        }

        public MockMealService MockGetMealFoodViewModel()
        {
            Setup(service => service.GetMealFoodViewModel(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MealFoodViewModel())
                .Verifiable();

            return this;
        }

        public MockMealService MockExistsFood(bool exists)
        {
            Setup(service => service.ExistsFood(It.IsAny<int>()))
                .ReturnsAsync(exists)
                .Verifiable();

            return this;
        }

        public MockMealService MockCreateFoodAsync()
        {
            Setup(service => service.CreateFoodAsync(It.IsAny<Food>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }

        public MockMealService MockCreateMealFoodAsync()
        {
            Setup(service => service.CreateMealFoodAsync(It.IsAny<MealFood>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }

        public MockMealService MockGetFoodByIdAsync(Food food)
        {
            Setup(service => service.GetFoodByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(food)
                .Verifiable();

            return this;
        }

        public MockMealService MockEditFoodAsync()
        {
            Setup(service => service.EditFoodAsync(It.IsAny<Food>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }

        public MockMealService MockDeleteMealFoodAsync()
        {
            Setup(service => service.DeleteMealFoodAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }

        public MockMealService MockGetById(Meal meal)
        {
            Setup(service => service.GetById(It.IsAny<int>()))
                .Returns(meal)
                .Verifiable();
            return this;
        }

        public MockMealService MockGetSesshinOwner(string userId)
        {
            Setup(service => service.GetMealSesshinOwner(It.IsAny<int>()))
                .Returns(userId)
                .Verifiable();
            return this;
        }
    }
}
