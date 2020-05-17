using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace WebFeatures.AppInitializer.Logging
{
    internal class LoggingDbCommand : IDbCommand
    {
        private readonly IDbCommand _decoratee;
        private readonly ILogger _logger;

        public LoggingDbCommand(IDbCommand decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        public string CommandText
        {
            get => _decoratee.CommandText;
            set => _decoratee.CommandText = value;
        }

        public int CommandTimeout
        {
            get => _decoratee.CommandTimeout;
            set => _decoratee.CommandTimeout = value;
        }

        public CommandType CommandType
        {
            get => _decoratee.CommandType;
            set => _decoratee.CommandType = value;
        }

        public IDbConnection Connection
        {
            get => _decoratee.Connection;
            set => _decoratee.Connection = value;
        }

        public IDataParameterCollection Parameters => _decoratee.Parameters;

        public IDbTransaction Transaction
        {
            get => _decoratee.Transaction;
            set => _decoratee.Transaction = value;
        }

        public UpdateRowSource UpdatedRowSource
        {
            get => _decoratee.UpdatedRowSource;
            set => _decoratee.UpdatedRowSource = value;
        }

        public void Cancel()
        {
            _decoratee.Cancel();
        }

        public IDbDataParameter CreateParameter()
        {
            return _decoratee.CreateParameter();
        }

        public void Dispose()
        {
            _decoratee.Dispose();
        }

        public int ExecuteNonQuery()
        {
            _logger.LogInformation(CommandText);

            try
            {
                return _decoratee.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Executing command error");
                throw;
            }
        }

        public IDataReader ExecuteReader()
        {
            return _decoratee.ExecuteReader();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return _decoratee.ExecuteReader(behavior);
        }

        public object ExecuteScalar()
        {
            return _decoratee.ExecuteScalar();
        }

        public void Prepare()
        {
            _decoratee.Prepare();
        }
    }
}
