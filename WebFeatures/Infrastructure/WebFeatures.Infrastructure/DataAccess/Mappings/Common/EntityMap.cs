using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using WebFeatures.Common;
using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal interface IEntityMap
    {
        TableMap Table { get; }
        PropertyMap Identity { get; }
        IEnumerable<PropertyMap> Properties { get; }
    }

    internal interface IEntityMap<TEntity> : IEntityMap
        where TEntity : Entity
    {
        PropertyMap GetProperty(Expression<Func<TEntity, object>> propertyCall);
    }

    internal class EntityMap<TEntity> : IEntityMap<TEntity>
        where TEntity : Entity
    {
        public TableMap Table
        {
            get => _table ?? (_table = new TableMap(GetConventionalTableName()));
            private set => _table = value;
        }
        private TableMap _table;

        public PropertyMap Identity => _identity;
        private PropertyMap _identity;

        public IEnumerable<PropertyMap> Properties => _properties;
        private readonly HashSet<PropertyMap> _properties;

        public EntityMap()
        {
            _properties = SqlType<TEntity>.Properties
                .Select(x => new PropertyMap(
                    x.Name,
                    typeof(TEntity).CreatePropertyAccessDelegate(x)))
                .ToHashSet();

            _identity = _properties.FirstOrDefault(x => x.Property == "Id");
        }

        public PropertyMap GetProperty(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            string property = propertyCall.GetPropertyName();

            PropertyMap map = _properties.FirstOrDefault(x => x.Property == property);

            return map;
        }

        protected TableMap.Builder ToTable(string table)
        {
            _table = new TableMap(table);

            return new TableMap.Builder(_table);
        }

        protected PropertyMap.Builder WithIdentity(Expression<Func<TEntity, object>> propertyCall)
        {
            RemoveProperty(ref _identity);

            var map = MapPropertyImpl(propertyCall);

            _identity = map.ResultMap;

            return map.Builder;
        }

        protected void WithoutIdentity()
        {
            RemoveProperty(ref _identity);
        }

        private void RemoveProperty(ref PropertyMap property)
        {
            if (property != null)
            {
                _properties.Remove(property);
            }

            property = null;
        }

        protected PropertyMap.Builder MapProperty(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            return MapPropertyImpl(propertyCall).Builder;
        }

        private (PropertyMap.Builder Builder, PropertyMap ResultMap) MapPropertyImpl(Expression<Func<TEntity, object>> propertyCall)
        {
            PropertyMap propertyMap = new PropertyMap(
                propertyCall.GetPropertyName(),
                propertyCall.Compile());

            _properties.Add(propertyMap);

            var builder = new PropertyMap.Builder(propertyMap);

            return (builder, propertyMap);
        }

        private string GetConventionalTableName()
        {
            string entityName = typeof(TEntity).Name;

            if (entityName.EndsWith("y"))
            {
                return entityName.Remove(entityName.Length - 1) + "ies";
            }

            if (entityName.EndsWith("s"))
            {
                return entityName + "es";
            }

            return entityName + "s";
        }
    }
}
