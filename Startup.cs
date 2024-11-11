using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AATTechnicalAssessment.Data;
using Microsoft.EntityFrameworkCore;

namespace AATTechnicalAssessment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds support for Razor Pages
            services.AddRazorPages();

            // Adds support for Blazor Server
            services.AddServerSideBlazor();

          
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))); // Ensure your connection string is configured in appsettings.json

            
            services.AddSingleton<WeatherForecastService>(); // Example singleton service

          
            services.AddSingleton<DataGenerator>();
            services.AddSingleton<FileExportService>();

          
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Use detailed error pages in development
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Use the error handler in non-development environments
                app.UseExceptionHandler("/Error");
                app.UseHsts(); // Enforce HTTP Strict Transport Security for security
            }

            
            app.UseHttpsRedirection();

           
            app.UseStaticFiles();

            // Enable routing for endpoint resolution
            app.UseRouting();

          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub(); // Maps the Blazor SignalR hub
                endpoints.MapFallbackToPage("/_Host"); // Specifies the fallback page
            });
        }
    }
}
