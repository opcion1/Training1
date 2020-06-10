using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;
using Training1.Tests.Utils;
using Xunit;

namespace Training1.Tests.Repositories
{
    public abstract class ModelBaseTestBase<T> : ProductDbContextTestBase where T : ModelBase
    {
        protected readonly T _testModel;
        protected readonly T _testModel2;

        public ModelBaseTestBase(T model, T model2)
        {
            _testModel = model;
            _testModel2 = model2;
        }


        protected async Task AddEntities(List<ModelBase> entities, DbContextOptions<ProductContext> options)
        {
            await Disposable.Using(
                () => new ProductContext(options),
                async context =>
                {
                    foreach (var entity in entities)
                    {
                        context.Add(entity);
                    }
                    await context.SaveChangesAsync();
                });
        }


        [Fact]
        public async Task Entity_AddAsync_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = GetRepository(context);
                    await repository.AddAsync((T)_testModel);
                    await context.SaveChangesAsync();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProductContext(options))
                {
                    var dbSet = context.Set<T>();
                    var entityCount = await dbSet.CountAsync();
                    var entity = await dbSet.SingleAsync();
                    Assert.Equal(1, entityCount);
                    AssertEntityEqual(entity, _testModel);
                }
            }
            finally
            {
                _connection.Close();
            }
        }
        [Fact]
        public async Task Entity_GetByIdAsync_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Add an identity to the in memory database
                await AddEntities(new List<ModelBase> { _testModel }, options);

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = GetRepository(context);
                    var entity = await repository.GetByIdAsync(1);
                    AssertEntityEqual(entity, _testModel);
                }
            }
            finally
            {
                _connection.Close();
            }
        }
        [Fact]
        public async Task Entity_ListAsync_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Add an identity to the in memory database
                await AddEntities(new List<ModelBase> { _testModel, _testModel2 }, options);

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = GetRepository(context);
                    var entity = await repository.ListAsync();
                }

                //Assert
                using (var context = new ProductContext(options))
                {
                    var dbSet = context.Set<T>();
                    var entityCount = await dbSet.CountAsync();
                    Assert.Equal(2, entityCount);
                }
            }
            finally
            {
                _connection.Close();
            }
        }
        [Fact]
        public async Task Entity_Update_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Add an identity to the in memory database
                await AddEntities(new List<ModelBase> { _testModel, _testModel2 }, options);

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = GetRepository(context);
                    var entity = await context.Set<T>().FirstOrDefaultAsync();
                    T updatedEntity = GetUpdatedEntity(entity);
                    repository.Update(updatedEntity);
                    await context.SaveChangesAsync();
                }

                //Assert
                using (var context = new ProductContext(options))
                {
                    var entity = await context.Set<T>().FirstOrDefaultAsync();
                    AssertEntityEqual(entity, _testModel2);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        [Fact]
        public async Task Entity_DeleteAsync_Test()
        {
            // In-memory database only exists while the connection is open

            _connection.Open();

            try
            {
                var options = CreateOptionAndEnsureCreated();

                // Add an identity to the in memory database
                await AddEntities(new List<ModelBase> { _testModel }, options);

                // Run the test against one instance of the context
                using (var context = new ProductContext(options))
                {
                    var repository = GetRepository(context);
                    DbSet<T> dbSet = context.Set<T>();
                    var entity = await dbSet.FirstOrDefaultAsync();
                    await repository.DeleteAsync(entity.Id);
                    await context.SaveChangesAsync();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ProductContext(options))
                {
                    DbSet<T> dbSet = context.Set<T>();
                    var entityCount = await dbSet.CountAsync();
                    Assert.Equal(0, entityCount);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        #region abstract methods
        public abstract EFRepositoryBase<T> GetRepository(ProductContext context);
        internal abstract void AssertEntityEqual(T entity1, T entity2);

        internal abstract T GetUpdatedEntity(T entity);
        #endregion
    }
}
