using Shouldly;
using System;
using System.Reflection;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.Tests.TestObjects;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.UnitTests.DataAccess.Mappings.Profiles
{
    public class EntityProfileTests
    {
        [Fact]
        public void GetMap_ShouldReturnMapForEntityWithMap()
        {
            // Arrange
            var profile = new EntityProfile();
            profile.AddMappingsFromAssembly(Assembly.GetExecutingAssembly());

            // Act
            IEntityMap<TestEntity> map = profile.GetMap<TestEntity>();

            // Assert
            map.ShouldNotBeNull();
        }

        [Fact]
        public void GetMap_ShouldThrowWhenMapIsMissing()
        {
            // Arrange
            var profile = new EntityProfile();

            // Assert
            Assert.Throws<InvalidOperationException>(() => profile.GetMap<TestEntity>());
        }
    }
}
