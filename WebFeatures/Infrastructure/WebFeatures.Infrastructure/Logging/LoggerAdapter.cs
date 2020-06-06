using Microsoft.Extensions.Logging;
using System;

namespace WebFeatures.Infrastructure.Logging
{
    internal class LoggerAdapter<T> : Application.Interfaces.Logging.ILogger<T>
    {
        private readonly ILogger<T> _innerLogger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _innerLogger = loggerFactory.CreateLogger<T>();
        }

        public void LogInformation(string message)
        {
            _innerLogger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _innerLogger.LogWarning(message);
        }

        public void LogError(string message, Exception exception)
        {
            _innerLogger.LogError(message, exception);
        }
    }
}