using Training1.Repositories;
using Training1.Repositories.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RespositoriesServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, EFProductRepository>();
            services.AddScoped<IStockRepository, EFStockRepository>();
            services.AddScoped<ISesshinRepository, EFSesshinRepository>();
            services.AddScoped<IFoodRepository, EFFoodRepository>();
            services.AddScoped<IMealFoodRepository, EFMealFoodRepository>();
            services.AddScoped<IDayOfSesshinRepository, EFDayOfSesshinRepository>();
            services.AddScoped<IMealRepository, EFMealRepository>();
            services.AddScoped<IIngredientRepository, EFIngredientRepository>();
            services.AddScoped<IAccountRepository, EFAccountRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
