using Xunit;

namespace WebFeatures.Infrastructure.Tests.Fixtures.Collections
{
    [CollectionDefinition("PostgreSqlDatabase")]
    public class PostgreSqlDatabaseCollection : ICollectionFixture<PostgreSqlDatabaseFixture>
    {
    }
}
