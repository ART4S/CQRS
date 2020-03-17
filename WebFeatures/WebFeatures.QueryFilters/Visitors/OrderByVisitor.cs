using System;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.AntlrGenerated;
using WebFeatures.QueryFilters.Helpers;
using WebFeatures.QueryFilters.Nodes;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class OrderByVisitor : QueryFiltersBaseVisitor<object>
    {
        private object _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public OrderByVisitor(object sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override object VisitOrderBy(QueryFiltersParser.OrderByContext context)
        {
            return context.expression.Accept(this);
        }

        public override object VisitOrderByExpression(QueryFiltersParser.OrderByExpressionContext context)
        {
            foreach (var atom in context.orderByAtom())
            {
                _sourceQueryable = atom.Accept(this);
            }

            return _sourceQueryable;
        }

        public override object VisitOrderByAtom(QueryFiltersParser.OrderByAtomContext context)
        {
            var property = new PropertyNode(context.propertyName.Text, _parameter).CreateExpression();

            var lambda = ReflectionCache.Lambda.MakeGenericMethod(
                typeof(Func<,>).MakeGenericType(_parameter.Type, property.Type));

            var expression = lambda.Invoke(null, new object[] { property, new ParameterExpression[] { _parameter } });

            var orderBy = (context.sortType == null || context.sortType.Type == QueryFiltersLexer.ASC ?
                    context.isFirstSort ? ReflectionCache.OrderBy : ReflectionCache.ThenBy :
                    context.isFirstSort ? ReflectionCache.OrderByDescending : ReflectionCache.ThenByDescending)
                .MakeGenericMethod(_parameter.Type, property.Type);

            return orderBy.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
