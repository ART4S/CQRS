using System;
using System.Data;

namespace WebFeatures.Infrastructure.Tests.Common.Fixtures
{
    public abstract class DatabaseFixture : IDisposable
    {
        public IDbConnection Connection
        {
            get
            {
                OpenConnection();
                return _connection;
            }
        }
        private readonly IDbConnection _connection;

        protected DatabaseFixture(IDbConnection connection)
        {
            _connection = connection;
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
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
