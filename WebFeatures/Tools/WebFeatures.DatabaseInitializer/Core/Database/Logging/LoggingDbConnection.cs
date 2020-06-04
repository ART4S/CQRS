using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace WebFeatures.DatabaseInitializer.Core.Database.Logging
{
    internal class LoggingDbConnection : IDbConnection
    {
        private readonly IDbConnection _decoratee;
        private readonly ILogger _logger;

        public LoggingDbConnection(IDbConnection decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        public void Open()
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

        public IDbTransaction BeginTransaction()
        {
            IDbTransaction transaction = _decoratee.BeginTransaction();

            _logger.LogInformation($"Transaction started");

            return new LoggingDbTransaction(transaction, _logger);
        }

        public IDbCommand CreateCommand()
        {
            IDbCommand command = _decoratee.CreateCommand();

            return new LoggingDbCommand(command, _logger);
        }

        public string ConnectionString
        {
            get => _decoratee.ConnectionString;
            set => _decoratee.ConnectionString = value;
        }

        public int ConnectionTimeout => _decoratee.ConnectionTimeout;
        public string Database => _decoratee.Database;
        public ConnectionState State => _decoratee.State;
        public IDbTransaction BeginTransaction(IsolationLevel il) => _decoratee.BeginTransaction(il);
        public void ChangeDatabase(string databaseName) => _decoratee.ChangeDatabase(databaseName);
        public void Close() => _decoratee.Close();
        public void Dispose() => _decoratee.Dispose();
    }
}
