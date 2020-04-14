using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Infrastructure;
using Training1.Infrastructure.Factory;
using Training1.Infrastructure.Validators;
using Training1.Models;
using Training1.Repositories;
using Training1.Repositories.Interfaces;
using Training1.Services;
using Training1.Services.Interfaces;

namespace Training1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddDefaultIdentity<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityContext>()
                .AddDefaultTokenProviders();

            // The DefaultModelMetadataProvider does significant caching and should be a singleton.
            services.TryAddSingleton<IModelMetadataProvider, DefaultModelMetadataProvider>();
            services.TryAdd(ServiceDescriptor.Transient<ICompositeMetadataDetailsProvider>(s =>
            {
                var options = s.GetRequiredService<IOptions<MvcOptions>>().Value;
                return new DefaultCompositeMetadataDetailsProvider(options.ModelMetadataDetailsProviders);
            }));

            services.AddSingleton<IEnumUtil, EnumUtil>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISesshinService, SesshinService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProductRepository, EFProductRepository>();
            services.AddScoped<IStockRepository, EFStockRepository>();
            services.AddScoped<ISesshinRepository, EFSesshinRepository>();
            services.AddScoped<IFoodRepository, EFFoodRepository>();
            services.AddScoped<IDayOfSesshinRepository, EFDayOfSesshinRepository>();
            services.AddScoped<IMealRepository, EFMealRepository>();
            services.AddScoped<IIngredientRepository, EFIngredientRepository>();
            services.AddScoped<IAccountRepository, EFAccountRepository>();
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AppClaimsPrincipalFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //scoped for this one because of the usermanager
            services.AddSingleton<IAuthorizationHandler,
                          AdminAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler,
                                  ChefAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler,
                                  AccountantAuthorizationHandler>();
            //services.AddTransient<IUserValidator<AppUser>, CustomAppUserValidator>();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ProductContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProductContext")));


            services.AddDbContext<AppIdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AppIdentityContextConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            
            GetMeSomeServiceLocator.Instance = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }


    public static class GetMeSomeServiceLocator
    {
        public static IServiceProvider Instance { get; set; }
    }
}
