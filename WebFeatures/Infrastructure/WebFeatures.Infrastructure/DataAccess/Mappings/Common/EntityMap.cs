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
            get
            {
                if (_table == null)
                {
                    string entityName = typeof(TEntity).Name;

                    if (entityName.EndsWith("y"))
                    {
                        _table = new TableMap(entityName.Remove(entityName.Length - 1) + "ies");
                    }
                    else
                    {
                        _table = new TableMap(entityName + "s");
                    }
                }

                return _table;
            }
            private set => _table = value;
        }
        private TableMap _table;

        public PropertyMap Identity { get; private set; } = new PropertyMap(nameof(Entity.Id));

        internal object Field()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PropertyMap> Mappings => _mappings;
        private readonly HashSet<PropertyMap> _mappings =
            SqlType<TEntity>.Properties
            .Select(x => new PropertyMap(x.Name))
            .ToHashSet();

        public TableMap ToTable(string table)
        {
            Guard.ThrowIfNullOrWhiteSpace(table, nameof(table));

            _table = new TableMap(table);

            return _table;
        }

        public PropertyMap SetIdentity(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            return Identity = MapProperty(propertyCall);
        }

        public void WithoutIdentity()
        {
            if (Identity != null)
            {
                _mappings.Remove(Identity);
            }

            Identity = null;
        }

        public PropertyMap MapProperty(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            string propertyName = propertyCall.GetPropetyName();

            PropertyMap map = new PropertyMap(propertyName);
            _mappings.Add(map);

            return map;
        }

        public PropertyMap GetPropertyMap(Expression<Func<TEntity, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            string propertyName = propertyCall.GetPropetyName();

            PropertyMap map = _mappings.FirstOrDefault(x => x.Property == propertyName);

            return map;
        }
    }
}
