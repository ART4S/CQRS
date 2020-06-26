using Bogus;
using FluentAssertions;
using System;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.Tests.Common.Stubs;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.DataAccess
{
    public class ReflectionExtensionsTests
    {
        [Fact]
        public void GetPropertyName_WhenPropertyIsReferenceType_ReturnsPropertyName()
        {
            // Arrange
            Expression<Func<CustomEntity, object>> propertyCall = x => x.StringProperty;

            string expected = nameof(CustomEntity.StringProperty);

            // Act
            string actual = propertyCall.GetPropertyName();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetPropertyName_WhenPropertyIsValueType_ReturnsPropertyName()
        {
            // Arrange
            Expression<Func<CustomEntity, object>> propertyCall = x => x.IntProperty;

            string expected = nameof(CustomEntity.IntProperty);

            // Act
            string actual = propertyCall.GetPropertyName();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetPropertyName_WhenMultiplePropertyCall_ReturnsPropertyName()
        {
            // Arrange
            Expression<Func<CustomEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            string expected = $"{nameof(CustomEntity.ValueObjectProperty)}_{nameof(CustomEntity.ValueObjectProperty.StringProperty)}";

            // Act
            string actual = propertyCall.GetPropertyName();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetPropertyName_WhenPropertyCallIsNull_Throws()
        {
            // Arrange
            Expression<Func<CustomEntity, object>> propertyCall = null;

            // Act
            Action actual = () => propertyCall.GetPropertyName();

            // Assert
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPropertyName_WhenFieldCall_Throws()
        {
            // Arrange
            Expression<Func<CustomEntity, object>> propertyCall = x => x.IntField;

            // Act
            Action actual = () => propertyCall.GetPropertyName();

            // Assert
            actual.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetFirstProperty_WhenPropertyCallExpression_ReturnsFirstProperty()
        {
            // Arrange
            Expression<Func<CustomEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            PropertyInfo expected = typeof(CustomEntity).GetProperty(nameof(CustomEntity.ValueObjectProperty));

            // Act
            PropertyInfo actual = propertyCall.GetFirstProperty();

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        public void CreateSetter_WhenPropertyInfoPropertyIsReferenceType_ReturnsPropertySetter()
        {
            // Arrange
            CustomEntity entity = new CustomEntity();

            PropertyInfo property = typeof(CustomEntity).GetProperty(nameof(CustomEntity.StringProperty));

            string expected = new Faker().Random.Utf16String();

            // Act
            Action<CustomEntity, object> propertySetter = property.CreateSetter<CustomEntity>();

            propertySetter(entity, expected);

            // Assert
            entity.StringProperty.Should().Be(expected);
        }

        [Fact]
        public void CreateSetter_WhenPropertyInfoPropertyIsValueType_ReturnsPropertySetter()
        {
            // Arrange
            CustomEntity entity = new CustomEntity();

            PropertyInfo property = typeof(CustomEntity).GetProperty(nameof(CustomEntity.IntProperty));

            int expected = new Faker().Random.Int();

            // Act
            Action<CustomEntity, object> propertySetter = property.CreateSetter<CustomEntity>();

            propertySetter(entity, expected);

            // Assert
            entity.IntProperty.Should().Be(expected);
        }

        [Fact]
        public void CreateSetter_WhenPropertyCallPropertyIsReferenceType_ReturnsPropertySetter()
        {
            // Arrange
            CustomEntity entity = new CustomEntity();

            Expression<Func<CustomEntity, object>> propertyCall = x => x.StringProperty;

            string expected = new Faker().Random.Utf16String();

            // Act
            Action<CustomEntity, object> propertySetter = propertyCall.CreateSetter();

            propertySetter(entity, expected);

            // Assert
            entity.StringProperty.Should().Be(expected);
        }

        [Fact]
        public void CreateSetter_WhenPropertyCallPropertyIsValueType_ReturnsPropertySetter()
        {
            // Arrange
            CustomEntity entity = new CustomEntity();

            Expression<Func<CustomEntity, object>> propertyCall = x => x.IntProperty;

            int expected = new Faker().Random.Int();

            // Act
            Action<CustomEntity, object> propertySetter = propertyCall.CreateSetter();

            propertySetter(entity, expected);

            // Assert
            entity.IntProperty.Should().Be(expected);
        }

        [Fact]
        public void CreateSetter_WhenMultiplePropertyCall_ReturnsPropertySetter()
        {
            // Arrange
            CustomEntity entity = new CustomEntity() { ValueObjectProperty = new CustomValueObject() };

            Expression<Func<CustomEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            string expected = new Faker().Random.Utf16String();

            // Act
            Action<CustomEntity, object> propertySetter = propertyCall.CreateSetter();

            propertySetter(entity, expected);

            // Assert
            entity.ValueObjectProperty.StringProperty.Should().Be(expected);
        }
    }
}