using Shouldly;
using System;
using System.Linq.Expressions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;
using WebFeatures.Infrastructure.Tests.TestObjects;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.UnitTests.DataAccess.Mappings.Utils
{
    public class ReflectionExtensionsTests
    {
        [Fact]
        public void GetPropertyName_ShouldReturnPropertyNameWhenPropertyIsRefType()
        {
            // Arrange
            Expression<Func<TestEntity, object>> refTypePropertyCall = x => x.StringProperty;

            // Act
            string propertyName = refTypePropertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe("StringProperty");
        }

        [Fact]
        public void GetPropertyName_ShouldReturnPropertyNameWhenPropertyIsValueType()
        {
            // Arrange
            Expression<Func<TestEntity, object>> valueTypePropertyCall = x => x.IntProperty;

            // Act
            string propertyName = valueTypePropertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe("IntProperty");
        }

        [Fact]
        public void GetPropertyName_ComplexPropertyCall_ShouldReturnPropertyCallChain()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            // Act
            string propertyName = propertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe("ValueObjectProperty_StringProperty");
        }

        [Fact]
        public void GetPropertyName_ShouldThrowWhenPassedNull()
        {
            Expression<Func<TestEntity, object>> propertyCall = null;

            Assert.Throws<ArgumentNullException>(() => propertyCall.GetPropertyName());
        }

        [Fact]
        public void GetPropertyName_ShouldThrowWhenInvalidPropertyCall()
        {
            Expression<Func<TestEntity, object>> invalidPropertyCall = x => x.IntField;

            Assert.Throws<InvalidOperationException>(() => invalidPropertyCall.GetPropertyName());
        }
    }
}
