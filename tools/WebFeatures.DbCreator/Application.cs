using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using WebFeatures.DbCreator.Core;
using WebFeatures.DbCreator.Core.DataAccess;
using WebFeatures.DbCreator.Core.DataAccess.Logging;

namespace WebFeatures.DbCreator
{
    internal class Application
    {
        public IConfiguration Configuration { get; }

        public IServiceProvider Services { get; }

        public Application()
        {
            Configuration = BuildConfiguration();
            Services = BuildServices();
        }

        private IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Settings.json");

            return builder.Build();
        }

        private IServiceProvider BuildServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(x => LoggerFactory.Create(builder =>
            {
                builder.ClearProviders();
                builder.AddConsole();
            }));

            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            string connectionString = Configuration.GetConnectionString("PostgreSql");

            services.AddSingleton<IDbConnectionFactory>(x =>
                new LoggingDbConnectionFactory(
                    new PostgreSqlDbConnectionFactory(connectionString),
                    x.GetService<ILoggerFactory>()));

            services.AddSingleton<ScriptsExecutor>();

            services.AddOptions();
            services.Configure<DbCreateOptions>(
                Configuration.GetSection(nameof(DbCreateOptions)));

            return services.BuildServiceProvider();
        }
    }
}