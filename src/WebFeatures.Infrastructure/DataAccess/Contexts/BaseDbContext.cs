using System;
using System.Data.Common;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class BaseDbContext : IDbContext, IAsyncDisposable
    {
        public DbConnection Connection => _connection.Value;
        private readonly Lazy<DbConnection> _connection;

        public BaseDbContext(IDbConnectionFactory connectionFactory)
        {
            _connection = new Lazy<DbConnection>(() =>
            {
                DbConnection connection = connectionFactory.CreateConnection();

                connection.Open();

                return connection;
            });
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection.IsValueCreated)
            {
                await _connection.Value.DisposeAsync();
            }
        }
    }
}