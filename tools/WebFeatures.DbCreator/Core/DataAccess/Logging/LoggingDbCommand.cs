using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.Common;

namespace WebFeatures.DbCreator.Core.DataAccess.Logging
{
    internal class LoggingDbCommand : DbCommand
    {
        private readonly DbCommand _decoratee;
        private readonly ILogger _logger;

        public LoggingDbCommand(DbCommand decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        public override int ExecuteNonQuery()
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

        public override string CommandText
        {
            get { return _decoratee.CommandText; }
            set { _decoratee.CommandText = value; }
        }
        public override int CommandTimeout
        {
            get { return _decoratee.CommandTimeout; }
            set { _decoratee.CommandTimeout = value; }
        }
        public override CommandType CommandType
        {
            get { return _decoratee.CommandType; }
            set { _decoratee.CommandType = value; }
        }

        public override bool DesignTimeVisible
        {
            get { return _decoratee.DesignTimeVisible; }
            set { _decoratee.DesignTimeVisible = value; }
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get { return _decoratee.UpdatedRowSource; }
            set { _decoratee.UpdatedRowSource = value; }
        }

        protected override DbConnection DbConnection
        {
            get { return _decoratee.Connection; }
            set { _decoratee.Connection = value; }
        }

        protected override DbParameterCollection DbParameterCollection => _decoratee.Parameters;

        protected override DbTransaction DbTransaction
        {
            get { return _decoratee.Transaction; }
            set { _decoratee.Transaction = value; }
        }

        public override void Cancel()
        {
            _decoratee.Cancel();
        }

        public override object ExecuteScalar()
        {
            return _decoratee.ExecuteScalar();
        }

        public override void Prepare()
        {
            _decoratee.Prepare();
        }

        protected override DbParameter CreateDbParameter()
        {
            return _decoratee.CreateParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return _decoratee.ExecuteReader(behavior);
        }
    }
}