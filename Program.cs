using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AATTechnicalAssessment
{
    public class Program
    {
        // The entry point of the application
        public static void Main(string[] args)
        {
            try
            {
                // Builds and runs the host
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                // Log the exception (you can add more complex logging here)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Configures and returns the IHostBuilder for the application
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); // Set up the startup class
                    // Optionally configure logging, URL bindings, or other services here
                });
    }
}
