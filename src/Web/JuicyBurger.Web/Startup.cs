using AutoMapper;
using CloudinaryDotNet;
using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Service;
using JuicyBurger.Service.Products;
using JuicyBurger.Services.Cloud;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Services.Orders;
using JuicyBurger.Services.Receipts;
using JuicyBurger.Web.Extensions;
using JuicyBurger.Web.InputModels.Products;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Reflection;

namespace JuicyBurger.Web
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

            services.AddDbContext<JuicyBurgerDbContext>(options =>
                options
                .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<JBUser, IdentityRole>()
                .AddEntityFrameworkStores<JuicyBurgerDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IReceiptService, ReceiptService>();
            services.AddSingleton<ICloudinaryService, CloudinaryService>();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = true;
            });

            Account accountCredentials = new Account(
                this.Configuration["Cloudinary:CloudName"], 
                this.Configuration["Cloudinary:ApiKey"], 
                this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(accountCredentials);
            services.AddSingleton(cloudinaryUtility);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
              typeof(Product).GetTypeInfo().Assembly,
              typeof(ProductsCreateInputModel).GetTypeInfo().Assembly,
              typeof(ProductViewModel).GetTypeInfo().Assembly,
              typeof(ProductServiceModel).GetTypeInfo().Assembly);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

          


            //The code below, fix floating point issue for double/decimal form field.
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<JuicyBurgerDbContext>())
                {
                    context.Database.EnsureCreated();

                    DatabaseSeed.CreateRoles(context);
                    DatabaseSeed.CreateOrderStatuses(context);
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}