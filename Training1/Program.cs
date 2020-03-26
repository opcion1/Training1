using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Training1.Areas.Identity.Data;
using Training1.Data;
using Training1.Models;

namespace Training1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                    var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
                    var identityContext = scope.ServiceProvider.GetRequiredService<AppIdentityContext>();
                    var productContext = services.GetRequiredService<ProductContext>();
                    
                    identityContext.Database.Migrate();
                    IdentitySeedData.EnsurePopulated(identityContext, roleManager, userManager);

                    productContext.Database.Migrate();
                    ProductSeedData.EnsurePopulated(productContext, identityContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
