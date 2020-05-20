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
    internal class EntityMap<TEntity> : IEntityMap
        where TEntity : BaseEntity
    {
        public Type Type => typeof(TEntity);

        public string Table
        {
            get
            {
                if (_table == null)
                {
                    if (Type.Name.EndsWith("y"))
                    {
                        _table = Type.Name.Remove(Type.Name.Length - 1) + "ies";
                    }
                    else
                    {
                        _table = Type.Name + "s";
                    }
                }

                return _table;
            }
            private set => _table = value;
        }
        private string _table;

        public PropertyMap Identity { get; private set; } = new PropertyMap(nameof(BaseEntity.Id));

        public IEnumerable<PropertyMap> Mappings => _mappings;
        private readonly HashSet<PropertyMap> _mappings =
            SqlType<TEntity>.Properties
            .Select(x => new PropertyMap(x.Name))
            .ToHashSet();

        public void ToTable(string table)
        {
            Guard.ThrowIfNullOrWhiteSpace(table, nameof(table));
            Table = table;
        }

        public PropertyMap SetIdentity(Expression<Func<TEntity, object>> propertyCall)
        {
            return Identity = MapProperty(propertyCall);
        }

        public PropertyMap MapProperty(Expression<Func<TEntity, object>> propertyCall)
        {
            var visitor = new PropertyNameVisitor();
            visitor.Visit(propertyCall);

            var map = new PropertyMap(visitor.PropertyName);
            _mappings.Add(map);

            return map;
        }
    }
}
