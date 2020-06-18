using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace WebFeatures.DbCreator.Core.DataAccess.Logging
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

        public DbConnection CreateConnection()
        {
            DbConnection connection = _decoratee.CreateConnection();
            ILogger logger = _loggerFactory.CreateLogger(connection.GetType().Name);

            return new LoggingDbConnection(connection, logger);
        }
    }
}
