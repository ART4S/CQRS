using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class BaseDbContext : IDbContext, IAsyncDisposable
    {
        public IDbConnection Connection => _connection.Value;
        private readonly Lazy<DbConnection> _connection;

        protected DbTransaction Transaction { get; private set; }

        public BaseDbContext(IDbConnectionFactory connectionFactory)
        {
            _connection = new Lazy<DbConnection>(() =>
            {
                DbConnection connection = connectionFactory.CreateConnection();
                connection.Open();

                Transaction = connection.BeginTransaction();

                return connection;
            });
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            if (Transaction == null) return;

            try
            {
                await Transaction.CommitAsync();
            }
            catch
            {
                await Transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await Transaction.DisposeAsync();

                Transaction = await _connection.Value.BeginTransactionAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (Transaction != null)
            {
                await Transaction.RollbackAsync();
                await Transaction.DisposeAsync();
            }

            if (_connection.IsValueCreated)
            {
                await _connection.Value.DisposeAsync();
            }
        }
    }
}