using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebFeatures.Domian.Common;
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
        public void GetMap_ShouldReturnMapForEntityWithManualMap()
        {
            // Arrange
            var profile = new EntityProfile();

            // Act
            IEntityMap<User> map = profile.GetMap<User>();

            // Assert
            map.ShouldNotBeNull();
        }

        [Fact]
        public void GetMap_ShouldReturnMapForEntityWithoutManualMap()
        {
            // Arrange
            var profile = new EntityProfile();

            // Act
            IEntityMap<TestEntity> map = profile.GetMap<TestEntity>();

            // Assert
            map.ShouldNotBeNull();
        }

        [Fact]
        public void GetMap_ShouldReturnMapForAllEntities()
        {
            // Arrange
            var profile = new EntityProfile();
            var entityTypes = typeof(User).Assembly
                .GetTypes()
                .Where(x => typeof(Entity).IsAssignableFrom(x) && !x.IsAbstract)
                .ToList();

            // Act
            var mappings = new List<object>();

            foreach (Type entityType in entityTypes)
            {
                MethodInfo getMapMethod = typeof(EntityProfile)
                    .GetMethod(
                        nameof(EntityProfile.GetMap),
                        BindingFlags.Public | BindingFlags.Instance)
                    .MakeGenericMethod(entityType);

                mappings.Add(getMapMethod.Invoke(profile, null));
            }

            // Assert
            entityTypes.Count.ShouldBe(mappings.Count);
            mappings.ShouldNotBeEmpty();
            mappings.ShouldNotContain((object)null);
        }
    }
}
