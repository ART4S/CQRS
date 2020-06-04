using Xunit;

namespace WebFeatures.Infrastructure.Tests.Helpers.Fixtures.Collections
{
    [CollectionDefinition("PostgreSqlDatabase")]
    public class PostgreSqlDatabaseCollection : ICollectionFixture<PostgreSqlDatabaseFixture>
    {
    }
}
