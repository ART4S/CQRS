using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
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

            await SetupDbContext(host);

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static async Task SetupDbContext(IWebHost host)
        {
            using var scope = host.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetService<WebFeaturesDbContext>();

                if (!context.Database.IsInMemory())
                {
                    await context.Database.EnsureCreatedAsync();

                    if ((await context.Database.GetPendingMigrationsAsync()).Any())
                    {
                        await context.Database.MigrateAsync();
                    }
                }

                var env = scope.ServiceProvider.GetService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    await context.SeedAsync(scope.ServiceProvider);
                }
            }
            catch (Exception e)
            {
                var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
                logger.LogError($"Error while setting context: {e.Message}", e);
            }
        }
    }
}
