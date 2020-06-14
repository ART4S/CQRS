using Dapper;
using Npgsql;
using WebFeatures.Infrastructure.Tests.Common.Utils;

namespace WebFeatures.Infrastructure.Tests.Common.Fixtures
{
    public class PostgreSqlDatabaseFixture : DatabaseFixture
    {
        private const string DatabaseName = "webfeatures_test_db";

        public PostgreSqlDatabaseFixture() : base(new NpgsqlConnection("server=localhost;port=5432;username=postgres;password=postgres"))
        {
            Init();
        }

        private void Init()
        {
            Connection.Execute(SqlBuilder.CloseExistingConnections(DatabaseName));
            Connection.Execute(SqlBuilder.DropDatabase(DatabaseName));
            Connection.Execute(SqlBuilder.CreateDatabase(DatabaseName));

            Connection.ChangeDatabase(DatabaseName);

            Connection.Execute(SqlBuilder.CreateSchema());

            DataSeeder.SeedTestData(Connection);

            Connection.Execute(SqlBuilder.RefreshAllViews());
        }

        public override void Dispose()
        {
            Connection.ChangeDatabase("postgres");

            Connection.Execute(SqlBuilder.CloseExistingConnections(DatabaseName));
            Connection.Execute(SqlBuilder.DropDatabase(DatabaseName));

            base.Dispose();
        }
    }
}