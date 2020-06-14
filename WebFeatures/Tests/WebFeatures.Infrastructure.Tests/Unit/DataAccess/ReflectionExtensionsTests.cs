using Shouldly;
using System;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.Tests.Helpers.TestObjects;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.DataAccess
{
    public class ReflectionExtensionsTests
    {
        [Fact]
        public void GetPropertyName_ShouldReturnPropertyName_WhenPropertyIsReferenceType()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.StringProperty;

            // Act
            string propertyName = propertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe(nameof(TestEntity.StringProperty));
        }

        [Fact]
        public void GetPropertyName_ShouldReturnPropertyName_WhenPropertyIsValueType()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.IntProperty;

            // Act
            string propertyName = propertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe(nameof(TestEntity.IntProperty));
        }

        [Fact]
        public void GetPropertyName_ShouldReturnPropertyName_ForMultiplePropertyCall()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            // Act
            string propertyName = propertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe($"{nameof(TestEntity.ValueObjectProperty)}_{nameof(TestEntity.ValueObjectProperty.StringProperty)}");
        }

        [Fact]
        public void GetPropertyName_ShouldThrow_WhenPassedNull()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = null;

            // Act
            Action actual = () => propertyCall.GetPropertyName();

            // Assert
            Assert.Throws<ArgumentNullException>(actual);
        }

        [Fact]
        public void GetPropertyName_ShouldThrow_ForNonPropertyCall()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.IntField;

            // Act
            Action actual = () => propertyCall.GetPropertyName();

            // Assert
            Assert.Throws<InvalidOperationException>(actual);
        }

        [Fact]
        public void GetFirstProperty_ShouldReturnFirstPropertyFromPropertyCallExpression()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            // Act
            PropertyInfo expected = typeof(TestEntity).GetProperty(nameof(TestEntity.ValueObjectProperty));
            PropertyInfo actual = propertyCall.GetFirstProperty();

            // Assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_ShouldCreatePropertySetterFromPropertyInfo_WhenPropertyIsReferenceType()
        {
            // Arrange
            TestEntity entity = new TestEntity();
            PropertyInfo property = typeof(TestEntity).GetProperty(nameof(TestEntity.StringProperty));

            // Act
            Action<TestEntity, object> propertySetter = property.CreateSetter<TestEntity>();

            string expected = "test";

            propertySetter(entity, expected);

            // Assert
            entity.StringProperty.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_ShouldCreatePropertySetterFromPropertyInfo_WhenPropertyIsValueType()
        {
            // Arrange
            TestEntity entity = new TestEntity();
            PropertyInfo property = typeof(TestEntity).GetProperty(nameof(TestEntity.IntProperty));

            // Act
            Action<TestEntity, object> propertySetter = property.CreateSetter<TestEntity>();

            int expected = 1;

            propertySetter(entity, expected);

            // Assert
            entity.IntProperty.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_ShouldCreatePropertySetterFromExpression_WhenPropertyIsReferenceType()
        {
            // Arrange
            TestEntity entity = new TestEntity();
            Expression<Func<TestEntity, object>> propertyCall = x => x.StringProperty;

            // Act
            Action<TestEntity, object> propertySetter = propertyCall.CreateSetter();

            string expected = "test";

            propertySetter(entity, expected);

            // Assert
            entity.StringProperty.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_ShouldCreatePropertySetterFromExpression_WhenPropertyIsValueType()
        {
            // Arrange
            TestEntity entity = new TestEntity();
            Expression<Func<TestEntity, object>> propertyCall = x => x.IntProperty;

            // Act
            Action<TestEntity, object> propertySetter = propertyCall.CreateSetter();

            int expected = 1;

            propertySetter(entity, expected);

            // Assert
            entity.IntProperty.ShouldBe(expected);
        }

        [Fact]
        public void CreateSetter_ShouldCreatPropertySetterFromExpression_ForMultiplePropertyCall()
        {
            // Arrange
            TestEntity entity = new TestEntity() { ValueObjectProperty = new TestValueObject() };
            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            // Act
            Action<TestEntity, object> propertySetter = propertyCall.CreateSetter();

            string expected = "test";

            propertySetter(entity, expected);

            // Assert
            entity.ValueObjectProperty.StringProperty.ShouldBe(expected);
        }
    }
}
