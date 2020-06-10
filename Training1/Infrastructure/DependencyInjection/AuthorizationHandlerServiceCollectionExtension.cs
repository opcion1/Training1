using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthorizationHandlerServiceCollectionExtension
    {
        public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler,
                          AdminAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler,
                                  ChefAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler,
                                  AccountantAuthorizationHandler>();
            return services;
        }
    }
}
