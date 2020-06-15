using Npgsql;
using Respawn;
using System;
using System.Data;
using System.Data.Common;

namespace WebFeatures.Infrastructure.Tests.Common.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public DbConnection Connection
        {
            get
            {
                OpenConnection();
                return _connection;
            }
        }
        private readonly DbConnection _connection;

        private readonly Checkpoint _checkpoint;

        public DatabaseFixture()
        {
            _connection = new NpgsqlConnection("server=localhost;port=5432;username=postgres;password=postgres;database=webfeatures_test_db");
            _checkpoint = new Checkpoint()
            {
                DbAdapter = DbAdapter.Postgres,
                SchemasToInclude = new[] { "public" },
                WithReseed = true,
            };
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void Reset()
        {
            _checkpoint.Reset(_connection);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}