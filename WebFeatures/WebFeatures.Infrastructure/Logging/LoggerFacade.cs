using Microsoft.Extensions.Logging;

namespace WebFeatures.Infrastructure.Logging
{
    public class LoggerFacade<T> : Application.Interfaces.Logging.ILogger<T>
    {
        private readonly ILogger<T> _innerLogger;

        public LoggerFacade(ILoggerFactory loggerFactory)
        {
            _innerLogger = loggerFactory.CreateLogger<T>();
        }

        public void LogInformation(string message, params object[] args)
        {
            _innerLogger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _innerLogger.LogWarning(message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _innerLogger.LogError(message, args);
        }
    }
}
