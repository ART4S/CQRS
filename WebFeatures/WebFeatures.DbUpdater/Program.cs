using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using WebFeatures.Application.Interfaces;
using WebFeatures.DataContext.Sql;
using WebFeatures.DbUpdater.Core;

namespace WebFeatures.DbUpdater
{
    class Program
    {
        public static void Main()
        {
            BuildServices().GetService<Updater>().Run();
        }

        private static ServiceProvider BuildServices()
        {
            var services = new ServiceCollection();

            var configuration = BuildConfiguration();
            services.AddSingleton(configuration);

            services.AddLogging(x => x.AddConsole());

            services.AddDbContext<IAppContext, SqlAppContext>();

            services.AddOptions();
            services.Configure<UpdaterOptions>(configuration.GetSection(nameof(UpdaterOptions)));

            services.AddSingleton<Updater>();

            return services.BuildServiceProvider();
        }

        private static IConfiguration BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration;
        }
    }
}
