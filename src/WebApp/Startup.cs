using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImageResizer.AspNetCore.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Data.Extentions;
using WebApp.Data.Interfaces;
using WebApp.Data.Repository;
using WebApp.Handlers;
using WebApp.Services;
using WebApp.Services.Interfaces;

namespace WebApp
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
            // Automapper Configuration 
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc();
            services.AddControllersWithViews();

            //AddImageResizer
            services.AddImageResizer();

            // Register Repository
            services.AddTransient<IPortfolioRepository, PortfolioRepository>();
            services.AddTransient<IPortfolioTypeRepository, PortfolioTypeRepository>();
            services.AddTransient<IPhotoRepository, PhotoRepository>();
            services.AddTransient<IMemberInterestRepository, MemberInterestRepository>();

            // Register Services
            services.AddTransient<IPortfolioService, PortfolioService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IUserService, UserService>();

            // Extensions
            services.AddTransient<IFileHandler, FileHandler>();

            // Add local AppClaimsPrincipalFactory class to customize user
            //services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ApplicationClaimsPrincipalFactory>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRights", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("MemberRights", policy => policy.RequireRole("Member", "Administrator"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthentication();
            app.UseAuthorization();
            //UseImageResizer
            app.UseImageResizer();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "adminRoute",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "membersRoute",
                    areaName: "Members",
                    pattern: "Members/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.ServiceProvider.GetService<ApplicationContext>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<ApplicationContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<ApplicationContext>().EnsureSeeded();
                }
            }
        }
    }
}
