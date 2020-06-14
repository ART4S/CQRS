using Microsoft.Extensions.Logging;

namespace WebFeatures.Infrastructure.Logging
{
    internal class LoggerFactory : Application.Interfaces.Logging.ILoggerFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public LoggerFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Application.Interfaces.Logging.ILogger<T> CreateLogger<T>()
        {
            return new LoggerAdapter<T>(_loggerFactory.CreateLogger<T>());
        }
    }
}
