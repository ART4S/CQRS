using System.Linq.Expressions;
using WebFeatures.QueryFilters.AntlrGenerated;
using WebFeatures.QueryFilters.Infrastructure;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class SkipVisitor : QueryFilteringBaseVisitor<object>
    {
        private readonly object _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public SkipVisitor(object sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override object VisitSkip(QueryFilteringParser.SkipContext context)
        {
            var skip = ReflectionCache.Skip
                .MakeGenericMethod(_parameter.Type);

            var count = int.Parse(context.count.Text);

            return skip.Invoke(null, new[] { _sourceQueryable, count });
        }
    }
}
