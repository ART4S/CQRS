using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using WebFeatures.Common;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Helpers;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal interface IEntityMap<TEntity> where TEntity : class
    {
        ITableMap Table { get; }
        IEnumerable<IPropertyMap<TEntity>> Properties { get; }
        IPropertyMap<TEntity> GetProperty(Expression<Func<TEntity, object>> propertyCall);
    }

    internal class EntityMap<TEntity> : IEntityMap<TEntity>
        where TEntity : class
    {
        public ITableMap Table => _table;
        private TableMap _table;

        public IEnumerable<IPropertyMap<TEntity>> Properties => _properties.Values;
        private readonly Dictionary<string, PropertyMap<TEntity>> _properties;

        public EntityMap()
        {
            _table = new TableMap(GetConventionalTableName());

            _properties = SqlType<TEntity>.Properties
                .Select(x => new PropertyMap<TEntity>(x))
                .ToDictionary(x => x.PropertyCall, x => x);
        }

        public IPropertyMap<TEntity> GetProperty(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            string propertyName = propertyCall.GetPropertyName();

            if (!_properties.TryGetValue(propertyName, out PropertyMap<TEntity> map))
            {
                throw new InvalidOperationException($"Property doesn't exists");
            }

            return map;
        }

        protected TableMap.Builder ToTable(string table)
        {
            _table = new TableMap(table);

            return new TableMap.Builder(_table);
        }

        protected PropertyMap<TEntity>.Builder MapProperty(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            var property = new PropertyMap<TEntity>(propertyCall);

            _properties[property.PropertyCall] = property;

            return new PropertyMap<TEntity>.Builder(property);
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