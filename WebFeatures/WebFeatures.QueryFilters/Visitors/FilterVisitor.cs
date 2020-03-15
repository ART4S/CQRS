using System;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.AntlrGenerated;
using WebFeatures.QueryFilters.Infrastructure;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class FilterVisitor : QueryFilteringBaseVisitor<object>
    {
        private readonly object _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public FilterVisitor(object sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override object VisitFilter(QueryFilteringParser.FilterContext context)
        {
            var visitor = new FilterExpressionVisitor(_parameter);
            var tree = context.expression.Accept(visitor);

            var lambda = ReflectionCache.Lambda
                .MakeGenericMethod(typeof(Func<,>)
                    .MakeGenericType(_parameter.Type, typeof(bool)));

            var expression = lambda.Invoke(null, new object[] { tree.CreateExpression(), new ParameterExpression[] { _parameter } });

            var where = ReflectionCache.Where
                .MakeGenericMethod(_parameter.Type);

            return where.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
