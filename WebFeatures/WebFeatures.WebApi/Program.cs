using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.DataContext;

namespace WebFeatures.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            await SeedData(host);

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) 
            => WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static async Task SeedData(IWebHost host)
        {
            using var scope = host.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetService<WebFeaturesDbContext>();
                await context.SeedAsync();
            }
            catch (Exception e)
            {
                var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
                logger.LogError($"Error while seeding data: {e.Message}");
            }
        }
    }
}
