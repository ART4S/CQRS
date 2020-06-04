using Shouldly;
using System;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;
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
            propertyName.ShouldBe("StringProperty");
        }

        [Fact]
        public void GetPropertyName_ShouldReturnPropertyName_WhenPropertyIsValueType()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.IntProperty;

            // Act
            string propertyName = propertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe("IntProperty");
        }

        [Fact]
        public void GetPropertyName_ShouldReturnPropertyName_ForMultiplePropertyCall()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            // Act
            string propertyName = propertyCall.GetPropertyName();

            // Assert
            propertyName.ShouldBe("ValueObjectProperty_StringProperty");
        }

        [Fact]
        public void GetPropertyName_ShouldThrow_WhenPassedNull()
        {
            Expression<Func<TestEntity, object>> propertyCall = null;

            Assert.Throws<ArgumentNullException>(() => propertyCall.GetPropertyName());
        }

        [Fact]
        public void GetPropertyName_ShouldThrow_ForNonPropertyCall()
        {
            Expression<Func<TestEntity, object>> propertyCall = x => x.IntField;

            Assert.Throws<InvalidOperationException>(() => propertyCall.GetPropertyName());
        }

        [Fact]
        public void GetFirstProperty_ShouldReturnFirstPropertyFromPropertyCallExpression()
        {
            // Arrange
            Expression<Func<TestEntity, object>> propertyCall = x => x.ValueObjectProperty.StringProperty;

            // Act
            PropertyInfo property = propertyCall.GetFirstProperty();

            // Assert
            property.ShouldBe(typeof(TestEntity)
                .GetProperty(nameof(TestEntity.ValueObjectProperty)));
        }

        [Fact]
        public void CreateSetter_ShouldCreatePropertySetterFromPropertyInfo_WhenPropertyIsReferenceType()
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
        public void CreateSetter_ShouldCreatePropertySetterFromPropertyInfo_WhenPropertyIsValueType()
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
        public void CreateSetter_ShouldCreatePropertySetterFromExpressio_WhenPropertyIsReferenceType()
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
        public void CreateSetter_ShouldCreatePropertySetterFromExpression_WhenPropertyIsValueType()
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
        public void CreateSetter_ShouldCreatPropertySetterFromExpression_ForMultiplePropertyCall()
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
