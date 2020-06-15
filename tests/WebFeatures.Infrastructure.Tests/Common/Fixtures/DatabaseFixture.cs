using Npgsql;
using Respawn;
using System;
using System.Data;
using WebFeatures.Infrastructure.Tests.Common.Utils;

namespace WebFeatures.Infrastructure.Tests.Common.Fixtures
{
    public class DatabaseFixture : IDisposable
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

        private readonly Checkpoint _checkpoint;

        public DatabaseFixture()
        {
            _connection = new NpgsqlConnection("server=localhost;port=5432;username=postgres;password=postgres;database=webfeatures_test_db");
            _checkpoint = new Checkpoint();

            Init();
        }

        private void Init()
        {
            DataSeeder.SeedTestData(Connection);
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
            _checkpoint.Reset(_connection.ConnectionString);
        }

        public void Dispose()
        {
            Connection.ChangeDatabase("postgres");

            Connection.Execute(SqlBuilder.CloseExistingConnections(DatabaseName));
        }
    }
}