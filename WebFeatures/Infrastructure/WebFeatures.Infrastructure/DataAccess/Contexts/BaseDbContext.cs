using System;
using System.Data;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class BaseDbContext : IDisposable, IDbContext
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

        public void SaveChanges()
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
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _transaction.Dispose();
                _transaction = null;

                _connection.Dispose();
                _connection = null;
            }
        }
    }
}