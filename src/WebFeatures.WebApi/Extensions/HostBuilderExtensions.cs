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
            string logFilePath = CreateLogFilePath();

            hostBuilder.UseSerilog((context, configuration) =>
            {
                configuration.WriteTo.Console(
                    restrictedToMinimumLevel: LogEventLevel.Information);

                configuration.WriteTo.File(
                    path: logFilePath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    restrictedToMinimumLevel: LogEventLevel.Information);
            });

            return hostBuilder;
        }

        private static string CreateLogFilePath()
        {
            string projectDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string logsDir = Path.Combine(projectDir, "Logs");

            if (!Directory.Exists(logsDir))
            {
                Directory.CreateDirectory(logsDir);
            }

            return Path.Combine(logsDir, "log.txt");
        }
    }
}