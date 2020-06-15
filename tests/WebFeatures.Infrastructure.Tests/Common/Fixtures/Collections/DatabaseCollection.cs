using Xunit;

namespace WebFeatures.Infrastructure.Tests.Common.Fixtures.Collections
{
    [CollectionDefinition(Name)]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        public const string Name = "Database";
    }
}
