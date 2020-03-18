using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.QueryFilters.AntlrGenerated;
using WebFeatures.QueryFilters.Helpers;
using WebFeatures.QueryFilters.Nodes;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class SelectVisitor : QueryFiltersBaseVisitor<IQueryable>
    {
        private readonly IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public SelectVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitSelect(QueryFiltersParser.SelectContext context)
        {
            return context.expression.Accept(this);
        }

        public override IQueryable VisitSelectExpression(QueryFiltersParser.SelectExpressionContext context)
        {
            var properties = context.PROPERTYACCESS()
                .Select(x => x.Symbol.Text)
                .ToHashSet();

            Type dictType = typeof(Dictionary<string, object>);
            MethodInfo addMethod = dictType.GetMethod("Add");

            ElementInit[] elementInitProperties = _parameter.Type
                .GetCashedProperties()
                .Where(p => properties.Contains(p.Name))
                .Select(p => Expression.ElementInit(
                    addMethod,
                    Expression.Constant(p.Name),
                    Expression.Convert(
                        new PropertyNode(p.Name, _parameter).CreateExpression(),
                        typeof(object))))
                .ToArray();

            ListInitExpression body = Expression.ListInit(
                Expression.New(dictType), elementInitProperties);

            MethodInfo lambda = ReflectionCache.Lambda.MakeGenericMethod(
                typeof(Func<,>).MakeGenericType(_parameter.Type, dictType));

            object expression = lambda.Invoke(null, new object[] { body, new ParameterExpression[] { _parameter } });

            MethodInfo select = ReflectionCache.Select
                .MakeGenericMethod(_parameter.Type, dictType);

            return (IQueryable)select.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
