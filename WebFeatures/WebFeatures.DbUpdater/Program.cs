using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using WebFeatures.DataContext;

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

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddSingleton(configuration);
            services.AddLogging(x => x.AddConsole());
            services.AddDbContext<AppContext>();
            services.AddSingleton<Updater>();

            return services.BuildServiceProvider();
        }
    }
}
