using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebFeatures.DbCreator.Core;

namespace WebFeatures.DbCreator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var app = new Application();
            var scripts = app.Services.GetRequiredService<ScriptsExecutor>();
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
