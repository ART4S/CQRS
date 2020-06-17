using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Common
{
    [Collection(DatabaseCollection.Name)]
    public class IntegrationTestBase
    {
        protected DatabaseFixture Database { get; }

        protected IntegrationTestBase(DatabaseFixture database)
        {
            Database = database;
        }
    }
}