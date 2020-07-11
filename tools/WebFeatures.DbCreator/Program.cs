using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebFeatures.DbCreator.Core;

namespace WebFeatures.DbCreator
{
	internal class Program
	{
		private static async Task Main(string[] args)
		{
			Application app = new Application();
			ScriptsExecutor scripts = app.Services.GetRequiredService<ScriptsExecutor>();
			var logger = app.Services.GetRequiredService<ILogger<Program>>();

			try
			{
				await scripts.ExecuteAsync();

				logger.LogInformation("Finished successfully");
			}
			catch (Exception e)
			{
				logger.LogError(e, "Finished with an exception");
			}
		}
	}
}
