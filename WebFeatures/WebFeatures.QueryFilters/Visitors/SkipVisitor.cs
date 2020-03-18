using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.QueryFilters.AntlrGenerated;
using WebFeatures.QueryFilters.Helpers;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class SkipVisitor : QueryFiltersBaseVisitor<IQueryable>
    {
        private readonly IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public SkipVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitSkip(QueryFiltersParser.SkipContext context)
        {
            MethodInfo skip = ReflectionCache.Skip
                .MakeGenericMethod(_parameter.Type);

            int count = int.Parse(context.count.Text);

            return (IQueryable)skip.Invoke(null, new object[] { _sourceQueryable, count });
        }
    }
}
