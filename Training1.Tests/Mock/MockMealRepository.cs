using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Training1.Models;
using Training1.Repositories;

namespace Training1.Tests.Mock
{
    public class MockMealRepository : Mock<IMealRepository>
    {

        public MockMealRepository MockGetById(int id, Meal meal)
        {
            Setup(repo => repo.GetById(id))
                .Returns(meal);
            return this;
        }

        public MockMealRepository MockGetSesshinOwner(Meal meal)
        {
            Setup(repo => repo.GetSesshinOwner(meal))
                .Returns("sesshin-owner");
            return this;
        }
    }
}
