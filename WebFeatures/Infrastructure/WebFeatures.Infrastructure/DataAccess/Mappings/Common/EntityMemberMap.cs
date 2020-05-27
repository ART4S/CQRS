using Dapper;
using System;
using System.Reflection;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal class EntityMemberMap<TEntity> : SqlMapper.IMemberMap
        where TEntity : class
    {
        private readonly IPropertyMap<TEntity> _propertyMap;

        public EntityMemberMap(IPropertyMap<TEntity> propertyMap)
        {
            _propertyMap = propertyMap;
        }

        public string ColumnName => _propertyMap.ColumnName;
        public Type MemberType => Property.PropertyType;
        public PropertyInfo Property => _propertyMap.Property;
        public FieldInfo Field => throw new NotImplementedException();
        public ParameterInfo Parameter => throw new NotImplementedException();
    }
}
