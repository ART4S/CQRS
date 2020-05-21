using System;
using System.Linq.Expressions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Helpers
{
    internal static class MappingExtensions
    {
        public static string NameWithSchema(this TableMap table)
        {
            return $"{table.Schema}.{table.Name}";
        }

        public static string Field<TEntity>(this EntityMap<TEntity> entity, Expression<Func<TEntity, object>> propertyCall) where TEntity : class
        {
            return entity.GetPropertyMap(propertyCall).Field;
        }

        public static string Property<TEntity>(this EntityMap<TEntity> entity, Expression<Func<TEntity, object>> propertyCall) where TEntity : class
        {
            return entity.GetPropertyMap(propertyCall).Property;
        }
    }
}
