using Shouldly;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.Tests.Common.Stubs;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.DataAccess
{
    public class EntityProfileTests
    {
        [Fact]
        public void GetMap_WhenEntityHasBeenRegisteredManually_ReturnsMap()
        {
            // Arrange
            var profile = new EntityProfile();

            profile.TryRegisterMap(typeof(TestEntityMap));

            // Act
            IEntityMap<TestEntity> map = profile.GetMap<TestEntity>();

            // Assert
            map.ShouldNotBeNull();
        }

        [Fact]
        public void GetMap_WhenEntityHasNotBeenRegisteredManually_ReturnsMap()
        {
            // Arrange
            var profile = new EntityProfile();

            // Act
            IEntityMap<TestEntity> map = profile.GetMap<TestEntity>();

            // Assert
            map.ShouldNotBeNull();
        }
    }
}