using Dapper;
using Npgsql;
using WebFeatures.Infrastructure.Tests.Helpers;

namespace WebFeatures.Infrastructure.Tests.Fixtures
{
    public class PostgreSqlDatabaseFixture : DatabaseFixture<NpgsqlConnection>
    {
        private const string _databaseName = "webfeatures_test_db";

        public PostgreSqlDatabaseFixture() : base(new NpgsqlConnection("server=localhost;port=5432;username=postgres;password=postgres"))
        {
            Init();
        }

        private void Init()
        {
            CreateDatabase();
            CreateSchema();
            SeedTestData();
        }

        private void CreateDatabase()
        {
            SqlQuery sql;

            sql = SqlBuilder.CloseExistingConnections(_databaseName);
            Connection.Execute(sql);

            sql = SqlBuilder.DropDatabase(_databaseName);
            Connection.Execute(sql);

            sql = SqlBuilder.CreateDatabase(_databaseName);
            Connection.Execute(sql);

            Connection.ChangeDatabase(_databaseName);
        }

        private void CreateSchema()
        {
            SqlQuery sql = SqlBuilder.CreateSchema();
            Connection.Execute(sql);
        }

        private void SeedTestData()
        {
            DataSeeder.SeedTestData(Connection);
        }

        public override void Dispose()
        {
            Connection.ChangeDatabase("postgres");

            SqlQuery sql;

            sql = SqlBuilder.CloseExistingConnections(_databaseName);
            Connection.Execute(sql);

            sql = SqlBuilder.DropDatabase(_databaseName);
            Connection.Execute(sql);

            base.Dispose();
        }
    }
}
