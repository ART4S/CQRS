using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class BaseDbContext : IDbContext, IAsyncDisposable
    {
        public IDbConnection Connection => _connection.Value;
        private readonly Lazy<DbConnection> _connection;

        private TransactionScope _scope;

        public BaseDbContext(IDbConnectionFactory connectionFactory)
        {
            _scope = new TransactionScope(
                TransactionScopeOption.RequiresNew,
                TransactionScopeAsyncFlowOption.Enabled);

            _connection = new Lazy<DbConnection>(() =>
            {
                var connection = connectionFactory.CreateConnection();
                connection.Open();

                return connection;
            });
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            _scope.Complete();
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection.IsValueCreated)
            {
                await _connection.Value.DisposeAsync();
            }

            _scope.Dispose();
        }
    }
}