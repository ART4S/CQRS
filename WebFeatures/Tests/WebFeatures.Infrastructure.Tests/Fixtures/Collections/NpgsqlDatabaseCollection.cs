using Xunit;

namespace WebFeatures.Infrastructure.Tests.Fixtures.Collections
{
    [CollectionDefinition("NpgsqlDatabase")]
    public class NpgsqlDatabaseCollection : ICollectionFixture<NpgsqlDatabaseFixture>
    {
    }
}
