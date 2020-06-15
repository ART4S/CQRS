using Microsoft.Extensions.Logging;
using System;

namespace WebFeatures.Infrastructure.Logging
{
    internal class LoggerAdapter<T> : Application.Interfaces.Logging.ILogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger.LogError(message, exception);
        }
    }
}