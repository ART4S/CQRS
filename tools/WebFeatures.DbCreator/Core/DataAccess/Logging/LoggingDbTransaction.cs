using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.Common;

namespace WebFeatures.DbCreator.Core.DataAccess.Logging
{
    internal class LoggingDbTransaction : DbTransaction
    {
        private readonly DbTransaction _decoratee;
        private readonly ILogger _logger;

        public LoggingDbTransaction(DbTransaction decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        public override void Commit()
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

        public override void Rollback()
        {
            _decoratee.Rollback();
            _logger.LogInformation("Transaction rolled back");
        }

        public override IsolationLevel IsolationLevel => _decoratee.IsolationLevel;
        protected override DbConnection DbConnection => _decoratee.Connection;
    }
}
