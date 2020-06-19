using Bogus;
using Shouldly;
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
            Expression<Func<TestEntity, object>> propertyCall = x => x.StringProperty;

            // Act
            string propertyName = propertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe(nameof(TestEntity.StringProperty));
        }

        [Fact]
        public void GetPropertyName_WhenPropertyIsValueType_ReturnsPropertyName()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.IntProperty;

            // Act
            string propertyName = propertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe(nameof(TestEntity.IntProperty));
        }

        [Fact]
        public void GetPropertyName_WhenMultiplePropertyCall_ReturnsPropertyName()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            string expected = $"{nameof(TestEntity.ValueObjectProperty)}_{nameof(TestEntity.ValueObjectProperty.StringProperty)}";

            // Act
            string actual = propertyCall.GetPropertyName();

            // Assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void GetPropertyName_WhenPropertyCallIsNull_Throws()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = null;

            // Act
            void actual() => propertyCall.GetPropertyName();

            // Assert
            Assert.Throws<ArgumentNullException>(actual);
        }

        [Fact]
        public void GetPropertyName_WhenFieldCall_Throws()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.IntField;

            // Act
            void actual() => propertyCall.GetPropertyName();

            // Assert
            Assert.Throws<InvalidOperationException>(actual);
        }

        [Fact]
        public void GetFirstProperty_WhenPropertyCallExpression_ReturnsFirstProperty()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            PropertyInfo expected = typeof(TestEntity).GetProperty(nameof(TestEntity.ValueObjectProperty));

            // Act
            PropertyInfo actual = propertyCall.GetFirstProperty();

            // Assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_WhenPropertyInfoPropertyIsReferenceType_ReturnsPropertySetter()
        {
            // Arrange
            TestEntity entity = new TestEntity();

            PropertyInfo property = typeof(TestEntity).GetProperty(nameof(TestEntity.StringProperty));

            string expected = new Faker().Random.Utf16String();

            // Act
            Action<TestEntity, object> propertySetter = property.CreateSetter<TestEntity>();

            propertySetter(entity, expected);

            // Assert
            entity.StringProperty.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_WhenPropertyInfoPropertyIsValueType_ReturnsPropertySetter()
        {
            // Arrange
            TestEntity entity = new TestEntity();

            PropertyInfo property = typeof(TestEntity).GetProperty(nameof(TestEntity.IntProperty));

            int expected = new Faker().Random.Int();

            // Act
            Action<TestEntity, object> propertySetter = property.CreateSetter<TestEntity>();

            propertySetter(entity, expected);

            // Assert
            entity.IntProperty.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_WhenPropertyCallPropertyIsReferenceType_ReturnsPropertySetter()
        {
            // Arrange
            TestEntity entity = new TestEntity();

            Expression<Func<TestEntity, object>> propertyCall = x => x.StringProperty;

            string expected = new Faker().Random.Utf16String();

            // Act
            Action<TestEntity, object> propertySetter = propertyCall.CreateSetter();

            propertySetter(entity, expected);

            // Assert
            entity.StringProperty.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_WhenPropertyCallPropertyIsValueType_ReturnsPropertySetter()
        {
            // Arrange
            TestEntity entity = new TestEntity();

            Expression<Func<TestEntity, object>> propertyCall = x => x.IntProperty;

            int expected = new Faker().Random.Int();

            // Act
            Action<TestEntity, object> propertySetter = propertyCall.CreateSetter();

            propertySetter(entity, expected);

            // Assert
            entity.IntProperty.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_WhenMultiplePropertyCall_ReturnsPropertySetter()
        {
            // Arrange
            TestEntity entity = new TestEntity() { ValueObjectProperty = new TestValueObject() };

            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            string expected = new Faker().Random.Utf16String();

            // Act
            Action<TestEntity, object> propertySetter = propertyCall.CreateSetter();

            propertySetter(entity, expected);

            // Assert
            entity.ValueObjectProperty.StringProperty.ShouldBe(expected);
        }
    }
}