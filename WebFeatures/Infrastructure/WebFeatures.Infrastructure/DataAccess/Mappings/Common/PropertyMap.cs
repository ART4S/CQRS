using System;
using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal interface IPropertyMap<TEntity> where TEntity : class
    {
        string PropertyName { get; }
        string Column { get; }
        object GetValue(TEntity entity);
    }

    internal partial class PropertyMap<TEntity> : IPropertyMap<TEntity> where TEntity : class
    {
        public string PropertyName { get; }
        public string Column { get; private set; }

        private readonly Func<TEntity, object> _propValueAccessor;

        public PropertyMap(string propName, Func<TEntity, object> propValueAccessor)
        {
            Guard.ThrowIfNull(propName, nameof(propName));

            PropertyName = Column = propName;

            _propValueAccessor = propValueAccessor;
        }

        public object GetValue(TEntity entity)
        {
            return _propValueAccessor(entity);
        }

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }
    }
}
