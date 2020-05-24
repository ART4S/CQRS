using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using WebFeatures.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal interface IEntityMap<TEntity> where TEntity : class
    {
        TableMap Table { get; }
        PropertyMap<TEntity> Identity { get; }
        IEnumerable<PropertyMap<TEntity>> Properties { get; }
        PropertyMap<TEntity> GetProperty(Expression<Func<TEntity, object>> propertyCall);
    }

    internal class EntityMap<TEntity> : IEntityMap<TEntity>
        where TEntity : class
    {
        public TableMap Table
        {
            get => _table ?? (_table = new TableMap(GetConventionalTableName()));
            private set => _table = value;
        }
        private TableMap _table;

        public PropertyMap<TEntity> Identity => _identity;
        private PropertyMap<TEntity> _identity;

        public IEnumerable<PropertyMap<TEntity>> Properties => _properties;
        private readonly HashSet<PropertyMap<TEntity>> _properties;

        public EntityMap()
        {
            _properties = SqlType<TEntity>.Properties
                .Select(x => new PropertyMap<TEntity>(
                    x.Name,
                    x.CreateAccessFunc<TEntity>()))
                .ToHashSet();

            _identity = _properties.FirstOrDefault(x => x.PropertyName == "Id");
        }

        public PropertyMap<TEntity> GetProperty(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            string property = propertyCall.GetPropertyName();

            PropertyMap<TEntity> map = _properties.FirstOrDefault(x => x.PropertyName == property);

            return map;
        }

        protected TableMap.Builder ToTable(string table)
        {
            _table = new TableMap(table);

            return new TableMap.Builder(_table);
        }

        protected PropertyMap<TEntity>.Builder WithIdentity(Expression<Func<TEntity, object>> propertyCall)
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

        private void RemoveProperty(ref PropertyMap<TEntity> property)
        {
            if (property != null)
            {
                _properties.Remove(property);
            }

            property = null;
        }

        protected PropertyMap<TEntity>.Builder MapProperty(Expression<Func<TEntity, object>> propAccess)
        {
            Guard.ThrowIfNull(propAccess, nameof(propAccess));

            return MapPropertyImpl(propAccess).Builder;
        }

        private (PropertyMap<TEntity>.Builder Builder, PropertyMap<TEntity> ResultMap) MapPropertyImpl(Expression<Func<TEntity, object>> propAccess)
        {
            var propertyMap = new PropertyMap<TEntity>(
                propAccess.GetPropertyName(),
                propAccess.Compile());

            _properties.Add(propertyMap);

            var builder = new PropertyMap<TEntity>.Builder(propertyMap);

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
