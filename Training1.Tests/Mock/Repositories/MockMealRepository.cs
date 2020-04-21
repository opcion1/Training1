using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Training1.Models;
using Training1.Repositories;
using Training1.Repositories.Interfaces;

namespace Training1.Tests.Mock.Repositories
{
    public class MockMealRepository : Mock<IMealRepository>
    {

        public MockMealRepository MockGetById(int id, Meal meal)
        {
            Setup(repo => repo.GetById(id))
                .Returns(meal);
            return this;
        }

        public MockMealRepository MockGetSesshinOwner(int mealId)
        {
            Setup(repo => repo.GetMealSesshinOwner(mealId))
                .Returns("sesshin-owner");
            return this;
        }
    }
}
