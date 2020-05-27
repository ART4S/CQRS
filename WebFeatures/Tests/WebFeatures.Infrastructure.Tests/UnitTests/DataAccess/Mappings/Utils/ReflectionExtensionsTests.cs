using Shouldly;
using System;
using System.Linq.Expressions;
using System.Reflection;
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

        [Fact]
        public void CreateSetter_ShouldCreateRefTypePropertySetterFromPropertyInfo()
        {
            // Arrange
            TestEntity entity = new TestEntity();
            string test = "test";
            PropertyInfo property = typeof(TestEntity).GetProperty(nameof(TestEntity.StringProperty));

            // Act
            Action<TestEntity, object> propertySetter = property.CreateSetter<TestEntity>();
            propertySetter(entity, test);

            // Assert
            entity.StringProperty.ShouldBe(test);
        }

        [Fact]
        public void CreateSetter_ShouldCreateValueTypePropertySetterFromPropertyInfo()
        {
            // Arrange
            TestEntity entity = new TestEntity();
            int test = 1;
            PropertyInfo property = typeof(TestEntity).GetProperty(nameof(TestEntity.IntProperty));

            // Act
            Action<TestEntity, object> propertySetter = property.CreateSetter<TestEntity>();
            propertySetter(entity, test);

            // Assert
            entity.IntProperty.ShouldBe(test);
        }

        [Fact]
        public void CreateSetter_ShouldCreateRefTypePropertySetterFromExpression()
        {
            // Arrange
            TestEntity entity = new TestEntity();
            string test = "test";
            Expression<Func<TestEntity, object>> propertyCall = x => x.StringProperty;

            // Act
            Action<TestEntity, object> propertySetter = propertyCall.CreateSetter();
            propertySetter(entity, test);

            // Assert
            entity.StringProperty.ShouldBe(test);
        }

        [Fact]
        public void CreateSetter_ShouldCreateValueTypePropertySetterFromExpression()
        {
            // Arrange
            TestEntity entity = new TestEntity();
            int test = 1;
            Expression<Func<TestEntity, object>> propertyCall = x => x.IntProperty;

            // Act
            Action<TestEntity, object> propertySetter = propertyCall.CreateSetter();
            propertySetter(entity, test);

            // Assert
            entity.IntProperty.ShouldBe(test);
        }

        [Fact]
        public void CreateSetter_ComplexPropertyCall_ShouldCreateRefTypePropertySetterFromExpression()
        {
            // Arrange
            TestEntity entity = new TestEntity() { ValueObjectProperty = new TestValueObject() };
            string test = "test";
            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            // Act
            Action<TestEntity, object> propertySetter = propertyCall.CreateSetter();
            propertySetter(entity, test);

            // Assert
            entity.ValueObjectProperty.StringProperty.ShouldBe(test);
        }
    }
}
