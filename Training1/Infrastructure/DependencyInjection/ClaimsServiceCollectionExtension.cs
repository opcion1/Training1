using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Infrastructure.Factory;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ClaimsServiceCollectionExtension
    {
        public static IServiceCollection AddCustomsClaims(this IServiceCollection services)
        {
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AppClaimsPrincipalFactory>();

            return services;
        }
    }
}
