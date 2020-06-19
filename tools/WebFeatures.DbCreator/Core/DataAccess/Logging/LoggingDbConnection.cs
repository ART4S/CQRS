using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.Common;

namespace WebFeatures.DbCreator.Core.DataAccess.Logging
{
    internal class LoggingDbConnection : DbConnection
    {
        private readonly DbConnection _decoratee;
        private readonly ILogger _logger;

        public LoggingDbConnection(DbConnection decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        public override void Open()
        {
            try
            {
                _decoratee.Open();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Oppening connection error");

                throw;
            }
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            DbTransaction transaction = _decoratee.BeginTransaction();

            _logger.LogInformation($"Transaction started");

            return new LoggingDbTransaction(transaction, _logger);
        }

        protected override DbCommand CreateDbCommand()
        {
            DbCommand command = _decoratee.CreateCommand();

            return new LoggingDbCommand(command, _logger);
        }

        public override string ConnectionString
        {
            get { return _decoratee.ConnectionString; }
            set { _decoratee.ConnectionString = value; }
        }
        public override string Database => _decoratee.Database;
        public override string DataSource => _decoratee.DataSource;
        public override string ServerVersion => _decoratee.ServerVersion;
        public override ConnectionState State => _decoratee.State;

        public override void ChangeDatabase(string databaseName)
        {
            _decoratee.ChangeDatabase(databaseName);
        }

        public override void Close()
        {
            _decoratee.Close();
        }
    }
}