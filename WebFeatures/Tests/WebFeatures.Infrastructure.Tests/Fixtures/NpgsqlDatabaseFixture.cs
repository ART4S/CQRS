using Dapper;
using Npgsql;
using System.IO;
using WebFeatures.Infrastructure.Tests.Helpers;

namespace WebFeatures.Infrastructure.Tests.Fixtures
{
    public class NpgsqlDatabaseFixture : DatabaseFixture<NpgsqlConnection>
    {
        private const string _databaseName = "webfeatures_test_db";

        public NpgsqlDatabaseFixture() : base(new NpgsqlConnection("server=localhost;port=5432;username=postgres;password=postgres"))
        {
            Init();
        }

        private void Init()
        {
            CreateDatabase();
            CreateSchema();
            SeedData();
        }

        private void CreateDatabase()
        {
            string sql = $"DROP DATABASE IF EXISTS {_databaseName};\n" +
                         $"CREATE DATABASE {_databaseName};";

            Connection.Execute(sql);

            Connection.ChangeDatabase(_databaseName);
        }

        private void CreateSchema()
        {
            string sql = File.ReadAllText("Scripts/Schema.sql");

            Connection.Execute(sql);
        }

        private void SeedData()
        {
            DataSeeder.SeedData(Connection);
        }

        public override void Dispose()
        {
            Connection.ChangeDatabase("postgres");

            string sql = "SELECT pg_terminate_backend(pid)\n" +
                         "FROM pg_stat_activity\n" +
                         $"WHERE datname = '{_databaseName}' AND pid <> pg_backend_pid();\n" +
                         $"DROP DATABASE {_databaseName};\n";

            Connection.Execute(sql);

            base.Dispose();
        }
    }
}
