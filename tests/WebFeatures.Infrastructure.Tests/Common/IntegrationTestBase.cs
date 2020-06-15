using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using WebFeatures.Infrastructure.Tests.Common.Fixtures.Collections;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Common
{
    [Collection(DatabaseCollection.Name)]
    public abstract class IntegrationTestBase
    {
        protected IntegrationTestBase(DatabaseFixture db)
        {
            db.Reset();
        }
    }
}