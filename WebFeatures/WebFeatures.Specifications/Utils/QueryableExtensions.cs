using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebFeatures.Specifications.Utils
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Where<T, TProp>(this IQueryable<T> sourceQueryable,
            Expression<Func<T, TProp>> propGetter,
            Expression<Func<TProp, bool>> propCondition)
        {
            var expr = propGetter.Combine(propCondition);
            return sourceQueryable.Where(expr);
        }
    }
}
