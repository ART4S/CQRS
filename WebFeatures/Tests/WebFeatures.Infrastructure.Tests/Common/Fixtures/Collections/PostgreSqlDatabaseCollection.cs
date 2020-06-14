using Xunit;

namespace WebFeatures.Infrastructure.Tests.Common.Fixtures.Collections
{
    [CollectionDefinition("PostgreSqlDatabase")]
    public class PostgreSqlDatabaseCollection : ICollectionFixture<PostgreSqlDatabaseFixture>
    {
    }
}
