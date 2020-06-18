using System.Data.Common;
using System.Threading.Tasks;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Common.Base
{
    [Collection(DatabaseCollection.Name)]
    public class IntegrationTestBase : IAsyncLifetime
    {
        private DbTransaction _transaction;

        protected DatabaseFixture Database { get; }

        protected IntegrationTestBase(DatabaseFixture database)
        {
            Database = database;
        }

        public async Task InitializeAsync()
        {
            _transaction = await Database.Connection.BeginTransactionAsync();
        }

        public async Task DisposeAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}