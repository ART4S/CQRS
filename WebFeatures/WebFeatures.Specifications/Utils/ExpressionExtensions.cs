using System;
using System.Linq;
using System.Linq.Expressions;
using WebFeatures.Specifications.Visitors;

namespace WebFeatures.Specifications.Utils
{
    internal static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
            => left.Merge(right, Expression.AndAlso);

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
            => left.Merge(right, Expression.OrElse);

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
            => Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters);

        public static Expression<TFunc> Merge<TFunc>(
            this Expression<TFunc> left,
            Expression<TFunc> right,
            Func<Expression, Expression, Expression> merge)
        {
            var map = left.Parameters
                .Select((param, index) => new { r = right.Parameters[index], l = param })
                .ToDictionary(x => x.r, x => x.l);

            var rightBody = new ParamsReplacerVisitor(map).Visit(right.Body);

            return Expression.Lambda<TFunc>(merge(left.Body, rightBody), left.Parameters);
        }

        public static Expression<Func<T, bool>> Combine<T, TProp>(
            this Expression<Func<T, TProp>> left,
            Expression<Func<TProp, bool>> right)
        {
            var body = new ExpressionReplacerVisitor(right.Parameters[0], left.Body).Visit(right.Body);
            return Expression.Lambda<Func<T, bool>>(body, left.Parameters[0]);
        }
    }
}
