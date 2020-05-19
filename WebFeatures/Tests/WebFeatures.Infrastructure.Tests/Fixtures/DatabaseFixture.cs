using System;
using System.Data;

namespace WebFeatures.Infrastructure.Tests.Fixtures
{
    public abstract class DatabaseFixture<TConnection> : IDisposable
        where TConnection : IDbConnection
    {
        public TConnection Connection
        {
            get
            {
                OpenConnection();
                return _connection;
            }
        }
        private readonly TConnection _connection;

        protected DatabaseFixture(TConnection connection)
        {
            _connection = connection;
        }

        private void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public virtual void Dispose()
        {
            _connection.Dispose();
        }
    }
}
