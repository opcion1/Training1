using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Services.Interfaces;

namespace Training1.Tests.Mock.Services
{
    public class MockIngredientService : Mock<IIngredientService>
    {
        public MockIngredientService()
        {
        }

        public MockIngredientService MockCreateAsync()
        {
            Setup(service => service.CreateAsync(It.IsAny<Ingredient>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return this;
        }

        public MockIngredientService MockGetByIdAsync(Ingredient ingredient)
        {
            Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ingredient)
                .Verifiable();

            return this;
        }
    }
}
