using System.Data.Common;
using System.Threading.Tasks;
using Npgsql;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Common.Fixtures
{
	public class DatabaseFixture : IAsyncLifetime
	{
		public DatabaseFixture()
		{
			Connection = new NpgsqlConnection(
				"server=localhost;port=5432;username=postgres;password=postgres;database=webfeatures_test_db");
		}

		public DbConnection Connection { get; }

		public async Task InitializeAsync()
		{
			await Connection.OpenAsync();
		}

		public async Task DisposeAsync()
		{
			await Connection.DisposeAsync();
		}
	}
}
