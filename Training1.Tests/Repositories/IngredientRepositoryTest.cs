using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;
using Xunit;

namespace Training1.Tests.Repositories
{
    public class IngredientRepositoryTest : ModelBaseTestBase<Ingredient>
    {
        public IngredientRepositoryTest()
            : base(new Ingredient { Food = new Food { Name = "Ratatouille" }, Product = new Product { Name = "Tomat" }, Quantity = 5, UnityType = UnityType.Kilogrammes },
                    new Ingredient { Food = new Food { Name = "Omelette" }, Product = new Product { Name = "Egg" }, Quantity = 150, UnityType = UnityType.Unity })
        {
        }


        [Fact]
        public async Task GetIngredientsByFoodId_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();
                // Add an identity to the in memory database
                AddEntities(new List<ModelBase> { _testModel, _testModel2 }, options);

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = new EFIngredientRepository(context);
                    var ingredients = await repository.GetIngredientsByFoodId(1);
                    Assert.Equal(1, ((ICollection<Ingredient>)ingredients).Count);
                    var ingredient = ingredients.Single();
                    Assert.Equal("Tomat", ingredient.Product.Name);
                    Assert.Equal(5, ingredient.Quantity);
                    Assert.Equal(UnityType.Kilogrammes, ingredient.UnityType);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        #region override method
        public override EFRepositoryBase<Ingredient> GetRepository(ProductContext context)
        {
            return new EFIngredientRepository(context);
        }

        internal override void AssertEntityEqual(Ingredient entity1, Ingredient entity2)
        {
            Assert.Equal(entity1.FoodId, entity2.FoodId);
            Assert.Equal(entity1.ProductId, entity2.ProductId);
            Assert.Equal(entity1.Quantity, entity2.Quantity);
            Assert.Equal(entity1.UnityType, entity2.UnityType);
        }

        internal override Ingredient GetUpdatedEntity(Ingredient entity)
        {
            entity.FoodId = _testModel2.FoodId;
            entity.ProductId = _testModel2.ProductId;
            entity.Quantity = _testModel2.Quantity;
            entity.UnityType = _testModel2.UnityType;

            return entity;
        }
        #endregion
    }
}
