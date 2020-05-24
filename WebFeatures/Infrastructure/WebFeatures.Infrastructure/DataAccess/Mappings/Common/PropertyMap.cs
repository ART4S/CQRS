using System;
using WebFeatures.Common;
using WebFeatures.Domian.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal partial class PropertyMap
    {
        public string Property { get; }
        public string Column { get; private set; }

        private readonly Delegate _valueProvider;

        public PropertyMap(string property, Delegate valueProvider)
        {
            Guard.ThrowIfNull(property, nameof(property));
            Property = Column = property;

            _valueProvider = valueProvider;
        }

        public object GetValue(object entity)
        {
            return _valueProvider.DynamicInvoke(entity);
        }

        public override int GetHashCode()
        {
            return Property.GetHashCode();
        }
    }

    //internal class PropertyMap<TEntity> : PropertyMap
    //    where TEntity : Entity
    //{
    //    public delegate object GetPropertyValueDelegate(TEntity entity);

    //    public PropertyMap(string property, GetPropertyValueDelegate getValue) : base(property, getValue)
    //    {

    //    }
    //}
}
