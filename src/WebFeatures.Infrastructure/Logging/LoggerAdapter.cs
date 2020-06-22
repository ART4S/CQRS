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

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogError(string message, Exception exception, params object[] args)
        {
            _logger.LogError(message, exception, args);
        }
    }
}