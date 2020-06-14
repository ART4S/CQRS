using Dapper;
using Npgsql;
using WebFeatures.Infrastructure.Tests.Common.Utils;

namespace WebFeatures.Infrastructure.Tests.Common.Fixtures
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
            Connection.Execute(
                SqlBuilder.CloseExistingConnections(_databaseName));

            Connection.Execute(
                SqlBuilder.DropDatabase(_databaseName));

            Connection.Execute(
                SqlBuilder.CreateDatabase(_databaseName));

            Connection.ChangeDatabase(_databaseName);

            Connection.Execute(
                SqlBuilder.CreateSchema());

            DataSeeder.SeedTestData(Connection);

            Connection.Execute(
                SqlBuilder.RefreshAllViews());
        }

        public override void Dispose()
        {
            Connection.ChangeDatabase("postgres");

            Connection.Execute(
                SqlBuilder.CloseExistingConnections(_databaseName));

            Connection.Execute(
                SqlBuilder.DropDatabase(_databaseName));

            base.Dispose();
        }
    }
}