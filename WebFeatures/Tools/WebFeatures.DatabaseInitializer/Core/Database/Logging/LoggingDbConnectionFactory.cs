using Microsoft.Extensions.Logging;
using System.Data;

namespace WebFeatures.DatabaseInitializer.Core.Database.Logging
{
    internal class LoggingDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDbConnectionFactory _decoratee;
        private readonly ILoggerFactory _loggerFactory;

        public LoggingDbConnectionFactory(IDbConnectionFactory decoratee, ILoggerFactory loggerFactory)
        {
            _decoratee = decoratee;
            _loggerFactory = loggerFactory;
        }

        public IDbConnection CreateConnection()
        {
            IDbConnection connection = _decoratee.CreateConnection();
            ILogger logger = _loggerFactory.CreateLogger(connection.GetType().Name);

            return new LoggingDbConnection(connection, logger);
        }
    }
}
