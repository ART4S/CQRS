using System.Linq;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.AntlrGenerated;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class QueryVisitor : QueryFiltersBaseVisitor<IQueryable>
    {
        private IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public QueryVisitor(IQueryable sourceQueryable)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = Expression.Parameter(sourceQueryable.ElementType);
        }

        public override IQueryable VisitQuery(QueryFiltersParser.QueryContext context)
        {
            foreach (var function in context.queryFunction())
            {
                _sourceQueryable = function.Accept(this);
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitQueryFunction(QueryFiltersParser.QueryFunctionContext context)
        {
            foreach (var child in context.children)
            {
                _sourceQueryable = child.Accept(this) ?? _sourceQueryable;
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitTop(QueryFiltersParser.TopContext context)
        {
            var visitor = new TopVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitSkip(QueryFiltersParser.SkipContext context)
        {
            var visitor = new SkipVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitWhere(QueryFiltersParser.WhereContext context)
        {
            var visitor = new WhereVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitOrderBy(QueryFiltersParser.OrderByContext context)
        {
            var visitor = new OrderByVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitSelect(QueryFiltersParser.SelectContext context)
        {
            var visitor = new SelectVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }
    }
}
