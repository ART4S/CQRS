using FluentAssertions;
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
            var sut = new EntityProfile();

            sut.TryRegisterMap(typeof(CustomEntityMap));

            // Act
            IEntityMap<CustomEntity> map = sut.GetMap<CustomEntity>();

            // Assert
            map.Should().NotBeNull();
        }

        [Fact]
        public void GetMap_WhenEntityHasNotBeenRegisteredManually_ReturnsMap()
        {
            // Arrange
            var sut = new EntityProfile();

            // Act
            IEntityMap<CustomEntity> map = sut.GetMap<CustomEntity>();

            // Assert
            map.Should().NotBeNull();
        }
    }
}