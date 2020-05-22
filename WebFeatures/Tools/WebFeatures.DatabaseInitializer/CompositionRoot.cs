using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using WebFeatures.AppInitializer.Database;
using WebFeatures.AppInitializer.Logging;

namespace WebFeatures.AppInitializer
{
    internal class CompositionRoot
    {
        public IConfiguration Configuration { get; }

        public IServiceProvider Services { get; }

        public CompositionRoot()
        {
            Configuration = BuildConfiguration();
            Services = BuildServices();
        }

        private IConfiguration BuildConfiguration()
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("Settings.json");

            return builder.Build();
        }

        private IServiceProvider BuildServices()
        {
            var services = new ServiceCollection();

            RegisterLogging(services);
            RegisterDataAccess(services);

            services.AddSingleton<ScriptsRunner>();

            return services.BuildServiceProvider();
        }

        private void RegisterLogging(ServiceCollection services)
        {
            services.AddSingleton(
                x => LoggerFactory.Create(
                    builder => builder.ClearProviders().AddConsole()));

            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        }

        private void RegisterDataAccess(ServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("Npgsql");

            services.AddSingleton<IDbConnectionFactory>(
                x => new LoggingDbConnectionFactory(
                    new NpgsqlDbConnectionFactory(connectionString),
                    x.GetService<ILoggerFactory>()));
        }
    }
}
