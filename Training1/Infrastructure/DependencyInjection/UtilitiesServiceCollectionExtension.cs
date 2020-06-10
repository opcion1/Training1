using Training1.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UtilitiesServiceCollectionExtension
    {
        public static IServiceCollection AddUtilities(this IServiceCollection services)
        {
            services.AddSingleton<IEnumUtil, EnumUtil>();

            return services;
        }
    }
}
