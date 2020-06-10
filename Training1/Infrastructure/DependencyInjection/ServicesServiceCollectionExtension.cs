using Training1.Services;
using Training1.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISesshinService, SesshinService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
