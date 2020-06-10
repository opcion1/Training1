using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Training1.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<GridConfiguration>(config.GetSection("GridConfiguration"));
            services.TryAddSingleton<IGridConfiguration>(sp =>
                sp.GetRequiredService<IOptions<GridConfiguration>>().Value);

            return services;
        }
    }
}
