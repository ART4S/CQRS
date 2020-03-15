using System.Linq;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.AntlrGenerated;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class QueryVisitor : QueryFilteringBaseVisitor<object>
    {
        private object _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public QueryVisitor(IQueryable sourceQueryable)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = Expression.Parameter(sourceQueryable.ElementType);
        }

        public override object VisitQuery(QueryFilteringParser.QueryContext context)
        {
            foreach (var parameter in context.queryParameter())
            {
                _sourceQueryable = parameter.Accept(this);
            }

            return _sourceQueryable;
        }

        public override object VisitQueryParameter(QueryFilteringParser.QueryParameterContext context)
        {
            foreach (var child in context.children)
            {
                _sourceQueryable = child.Accept(this) ?? _sourceQueryable;
            }

            return _sourceQueryable;
        }

        public override object VisitTop(QueryFilteringParser.TopContext context)
        {
            var visitor = new TopVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override object VisitSkip(QueryFilteringParser.SkipContext context)
        {
            var visitor = new SkipVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override object VisitFilter(QueryFilteringParser.FilterContext context)
        {
            var visitor = new FilterVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override object VisitOrderBy(QueryFilteringParser.OrderByContext context)
        {
            var visitor = new OrderByVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override object VisitSelect(QueryFilteringParser.SelectContext context)
        {
            var visitor = new SelectVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }
    }
}
