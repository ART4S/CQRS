using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.IO;
using System.Reflection;

namespace WebFeatures.WebApi.Extensions
{
    internal static class HostBuilderExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, configuration) =>
            {
                configuration.WriteTo.Console(
                    restrictedToMinimumLevel: LogEventLevel.Information);

                string projectDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string logsDir = Path.Combine(projectDir, "Logs");

                Directory.CreateDirectory(logsDir);

                string logFilePath = Path.Combine(logsDir, "log.txt");

                configuration.WriteTo.File(
                    path: logFilePath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    restrictedToMinimumLevel: LogEventLevel.Warning);
            });

            return hostBuilder;
        }
    }
}
