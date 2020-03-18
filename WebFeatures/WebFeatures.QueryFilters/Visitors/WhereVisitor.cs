using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.QueryFilters.AntlrGenerated;
using WebFeatures.QueryFilters.Helpers;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class WhereVisitor : QueryFiltersBaseVisitor<IQueryable>
    {
        private readonly IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public WhereVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitWhere(QueryFiltersParser.WhereContext context)
        {
            var visitor = new WhereExpressionVisitor(_parameter);
            BaseNode whereExpressionNode = context.expression.Accept(visitor);

            MethodInfo lambda = ReflectionCache.Lambda
                .MakeGenericMethod(typeof(Func<,>)
                    .MakeGenericType(_parameter.Type, typeof(bool)));

            object expression = lambda.Invoke(null, new object[] { whereExpressionNode.CreateExpression(), new ParameterExpression[] { _parameter } });

            MethodInfo where = ReflectionCache.Where
                .MakeGenericMethod(_parameter.Type);

            return (IQueryable)where.Invoke(null, new object[] { _sourceQueryable, expression });
        }
    }
}
