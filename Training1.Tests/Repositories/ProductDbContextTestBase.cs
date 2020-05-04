using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Training1.Models;

namespace Training1.Tests.Repositories
{
    public abstract class ProductDbContextTestBase
    {
        protected readonly SqliteConnection _connection;

        public ProductDbContextTestBase()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
        }

        protected DbContextOptions<ProductContext> CreateOptionAndEnsureCreated()
        {
            var options = CreateOption();
            EnsureCreated(options);

            return options;
        }


        protected DbContextOptions<ProductContext> CreateOption()
        {
            return new DbContextOptionsBuilder<ProductContext>()
                    .UseSqlite(_connection)
                    .Options;
        }

        protected void EnsureCreated(DbContextOptions<ProductContext> options)
        {
            using (var context = new ProductContext(options))
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
