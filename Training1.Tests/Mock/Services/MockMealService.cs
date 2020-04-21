using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Training1.Models;
using Training1.Services.Interfaces;

namespace Training1.Tests.Mock.Services
{
    public class MockMealService : Mock<IMealService>
    {
        internal void MockGetById(int v, Meal meal)
        {
            throw new NotImplementedException();
        }

        internal void MockGetSesshinOwner(int id)
        {
            throw new NotImplementedException();
        }
    }
}
