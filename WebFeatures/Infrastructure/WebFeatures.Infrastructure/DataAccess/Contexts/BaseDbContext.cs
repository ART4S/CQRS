using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class BaseDbContext : IDbContext, IDisposable
    {
        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = _connectionFactory.CreateConnection();
                    _connection.Open();
                }

                if (_transaction == null)
                {
                    _transaction = _connection.BeginTransaction();
                }

                return _connection;
            }
        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly IDbConnectionFactory _connectionFactory;

        public BaseDbContext(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_connection == null)
            {
                throw new InvalidOperationException("Connection isn't created");
            }

            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}