using System;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal interface IPropertyMap<TEntity> where TEntity : class
    {
        string ColumnName { get; }
        string PropertyCall { get; }
        PropertyInfo Property { get; }
        object GetValue(TEntity entity);
        void SetValue(TEntity entity, object value);
    }

    internal partial class PropertyMap<TEntity> : IPropertyMap<TEntity> where TEntity : class
    {
        public string ColumnName { get; private set; }
        public string PropertyCall { get; }
        public PropertyInfo Property { get; }

        private Func<TEntity, object> _getter;
        private Action<TEntity, object> _setter;

        public PropertyMap(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            PropertyCall = ColumnName = propertyCall.GetPropertyName();
            Property = propertyCall.GetFirstProperty();

            _getter = propertyCall.Compile();
            _setter = propertyCall.CreateSetter();
        }

        public PropertyMap(PropertyInfo property)
        {
            Guard.ThrowIfNull(property, nameof(property));

            PropertyCall = ColumnName = property.Name;
            Property = property;

            _getter = property.CreateGetter<TEntity>();
            _setter = property.CreateSetter<TEntity>();
        }

        public object GetValue(TEntity entity)
        {
            return _getter(entity);
        }

        public void SetValue(TEntity entity, object value)
        {
            _setter(entity, value);
        }
    }
}
