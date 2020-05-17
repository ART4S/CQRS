using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace WebFeatures.AppInitializer.Logging
{
    internal class LoggingDbTransaction : IDbTransaction
    {
        private readonly IDbTransaction _decoratee;
        private readonly ILogger _logger;

        public LoggingDbTransaction(IDbTransaction decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        public IDbConnection Connection => _decoratee.Connection;

        public IsolationLevel IsolationLevel => _decoratee.IsolationLevel;

        public void Commit()
        {
            try
            {
                _decoratee.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Commit transaction error");
                throw;
            }

            _logger.LogInformation("Transaction finished");
        }

        public void Dispose()
        {
            _decoratee.Dispose();
        }

        public void Rollback()
        {
            _decoratee.Rollback();
            _logger.LogInformation("Transaction rolled back");
        }
    }
}
