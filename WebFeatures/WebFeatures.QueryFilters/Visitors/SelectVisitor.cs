using System;
using System.Linq;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.AntlrGenerated;
using WebFeatures.QueryFilters.Infrastructure;
using WebFeatures.QueryFilters.Nodes;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class SelectVisitor : QueryFilteringBaseVisitor<object>
    {
        private readonly object _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public SelectVisitor(object sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override object VisitSelect(QueryFilteringParser.SelectContext context)
        {
            return context.expression.Accept(this);
        }

        public override object VisitSelectExpression(QueryFilteringParser.SelectExpressionContext context)
        {
            var properties = context.PROPERTYACCESS()
                .Select(x => x.Symbol.Text)
                .ToHashSet();

            var typeProperties = _parameter.Type
                .GetCashedProperties()
                .Where(p => properties.Contains(p.Name))
                .ToDictionary(p => p.Name, p => p.PropertyType);

            var dynamicType = DynamicTypeBuilder.CreateDynamicType(typeProperties);

            var propExpressions = properties.ToDictionary(x => x, x => new PropertyNode(x, _parameter).CreateExpression());

            var body = Expression.MemberInit(
                Expression.New(dynamicType.GetConstructors().Single()),
                dynamicType.GetFields().Select(f => Expression.Bind(f, propExpressions[f.Name])));

            var lambda = ReflectionCache.Lambda.MakeGenericMethod(
                typeof(Func<,>).MakeGenericType(_parameter.Type, dynamicType));

            var expression = lambda.Invoke(null, new object[] { body, new ParameterExpression[] { _parameter } });

            var select = ReflectionCache.Select
                .MakeGenericMethod(_parameter.Type, dynamicType);

            return select.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
