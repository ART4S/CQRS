using System.Data.Common;
using System.Threading.Tasks;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Common.Base
{
	public class IntegrationTestBase : IAsyncLifetime
	{
		protected DatabaseFixture Database { get; private set; }
		protected DbTransaction Transaction { get; private set; }

		public async Task InitializeAsync()
		{
			Database = new DatabaseFixture();

			await Database.InitializeAsync();

			Transaction = await Database.Connection.BeginTransactionAsync();
		}

		public async Task DisposeAsync()
		{
			await Transaction.RollbackAsync();
			await Transaction.DisposeAsync();

			await Database.DisposeAsync();
		}
	}
}
