using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebFeatures.DbCreator.Core;
using WebFeatures.DbCreator.Core.DataAccess;
using WebFeatures.DbCreator.Core.DataAccess.Logging;

namespace WebFeatures.DbCreator
{
	internal class Application
	{
		public Application()
		{
			Init();
		}

		private void Init()
		{
			IConfiguration configuration = BuildConfiguration();
			
			Services = BuildServices(configuration);
		}
		
		public IServiceProvider Services { get; private set; }

		private static IConfiguration BuildConfiguration()
		{
			IConfigurationBuilder builder = new ConfigurationBuilder()
			   .SetBasePath(Directory.GetCurrentDirectory())
			   .AddJsonFile("Settings.json");

			return builder.Build();
		}

		private IServiceProvider BuildServices(IConfiguration configuration)
		{
			ServiceCollection services = new ServiceCollection();

			services.AddSingleton(x => LoggerFactory.Create(builder =>
			{
				builder.ClearProviders();
				builder.AddConsole();
			}));

			services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

			string connectionString = configuration.GetConnectionString("PostgreSql");

			services.AddSingleton<IDbConnectionFactory>(x =>
				new LoggingDbConnectionFactory(
					new PostgreSqlDbConnectionFactory(connectionString),
					x.GetService<ILoggerFactory>()));

			services.AddSingleton<ScriptsExecutor>();

			services.AddOptions();
			services.Configure<DbCreateOptions>(
				configuration.GetSection(nameof(DbCreateOptions)));

			return services.BuildServiceProvider();
		}
	}
}
