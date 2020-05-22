using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using WebFeatures.Common;
using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Helpers;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal class EntityMap<TEntity> where TEntity : class
    {
        public TableMap Table
        {
            get => _table ?? (_table = new TableMap(GetConventionalTableName()));
            private set => _table = value;
        }
        private TableMap _table;

        public PropertyMap Identity
        {
            get => _identity ?? (_identity = MapProperty(nameof(Entity.Id)).ResultMap);
            private set => _identity = value;
        }
        private PropertyMap _identity;

        public IEnumerable<PropertyMap> Mappings => _mappings;
        private readonly HashSet<PropertyMap> _mappings =
            SqlType<TEntity>.Properties
            .Select(x => new PropertyMap(x.Name))
            .ToHashSet();

        public PropertyMap GetPropertyMap(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            string propertyName = propertyCall.GetPropetyName();

            PropertyMap map = _mappings.FirstOrDefault(x => x.Property == propertyName);

            return map;
        }

        protected TableMap.Builder ToTable(string table)
        {
            Guard.ThrowIfNullOrWhiteSpace(table, nameof(table));

            _table = new TableMap(table);

            return new TableMap.Builder(_table);
        }

        protected PropertyMap.Builder SetIdentity(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

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
                _mappings.Remove(property);
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
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            string propertyName = propertyCall.GetPropetyName();

            return MapProperty(propertyName);
        }

        private (PropertyMap.Builder Builder, PropertyMap ResultMap) MapProperty(string propertyName)
        {
            PropertyMap property = new PropertyMap(propertyName);
            _mappings.Add(property);

            var builder = new PropertyMap.Builder(property);

            return (builder, property);
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
