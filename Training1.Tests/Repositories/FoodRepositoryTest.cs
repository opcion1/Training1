using Training1.Models;
using Training1.Repositories;
using Xunit;

namespace Training1.Tests.Repositories
{
    public class FoodRepositoryTest : ModelBaseTestBase<Food>
    {
        public FoodRepositoryTest()
            : base(new Food { Name = "Food 1", Description = "Description 1", NumberOfPeople = 50, Commentary = "Commentary 1" },
                    new Food { Name = "Food 2", Description = "Description 2", NumberOfPeople = 60, Commentary = "Commentary 2" })
        {
        }

        #region override method
        public override EFRepositoryBase<Food> GetRepository(ProductContext context)
        {
            return new EFFoodRepository(context);
        }

        internal override void AssertEntityEqual(Food entity1, Food entity2)
        {
            Assert.Equal(entity1.Name, entity2.Name);
            Assert.Equal(entity1.Description, entity2.Description);
            Assert.Equal(entity1.NumberOfPeople, entity2.NumberOfPeople);
            Assert.Equal(entity1.Commentary, entity2.Commentary);
        }

        internal override Food GetUpdatedEntity(Food entity)
        {
            entity.Name = _testModel2.Name;
            entity.Description = _testModel2.Description;
            entity.NumberOfPeople = _testModel2.NumberOfPeople;
            entity.Commentary = _testModel2.Commentary;

            return entity;
        }
        #endregion
    }
}
