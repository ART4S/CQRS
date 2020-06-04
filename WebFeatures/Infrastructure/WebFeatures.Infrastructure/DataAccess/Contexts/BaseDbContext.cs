using System;
using System.Data;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class BaseDbContext : IDisposable, IDbContext
    {
        public IDbConnection Connection => _connection.Value;
        private readonly Lazy<IDbConnection> _connection;

        public BaseDbContext(IDbConnectionFactory connectionFactory)
        {
            _connection = new Lazy<IDbConnection>(() =>
            {
                IDbConnection connection = connectionFactory.CreateConnection();
                connection.Open();

                return connection;
            });
        }

        public IDbTransaction BeginTransaction()
        {
            return _connection.Value.BeginTransaction();
        }

        public void Dispose()
        {
            if (_connection.IsValueCreated)
            {
                _connection.Value.Dispose();
            }
        }
    }
}
