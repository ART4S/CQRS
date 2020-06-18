using Npgsql;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Common.Fixtures
{
    [CollectionDefinition(Name)]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        public const string Name = "Database";
    }

    public class DatabaseFixture : IAsyncLifetime
    {
        public DbConnection Connection { get; }

        public DatabaseFixture()
        {
            Connection = new NpgsqlConnection(
                "server=localhost;port=5432;username=postgres;password=postgres;database=webfeatures_test_db");
        }

        public async Task InitializeAsync()
        {
            if (Connection.State != ConnectionState.Open)
            {
                await Connection.OpenAsync();
            }
        }

        public async Task DisposeAsync()
        {
            await Connection.DisposeAsync();
        }
    }
}