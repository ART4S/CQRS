using Microsoft.Extensions.Logging;

namespace WebFeatures.Infrastructure.Logging
{
    internal class LoggerFactoryAdapter : Application.Interfaces.Logging.ILoggerFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public LoggerFactoryAdapter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Application.Interfaces.Logging.ILogger<T> CreateLogger<T>()
        {
            return new LoggerAdapter<T>(_loggerFactory.CreateLogger<T>());
        }
    }
}
