using System;
using System.Linq;
using System.Linq.Expressions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Helpers
{
    internal static class EntityMapExtensions
    {
        public static PropertyMap GetMapFor<TEntity>(this IEntityMap entity, Expression<Func<TEntity, object>> propertyCall)
        {
            var visitor = new PropertyNameVisitor();
            visitor.Visit(propertyCall);

            PropertyMap map = entity.Mappings.FirstOrDefault(x => x.Property == visitor.PropertyName);
            return map;
        }
    }
}
