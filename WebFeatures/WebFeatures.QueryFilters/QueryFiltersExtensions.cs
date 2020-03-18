using Antlr4.Runtime;
using System.Linq;
using System.Runtime.CompilerServices;
using WebFeatures.QueryFilters.AntlrGenerated;
using WebFeatures.QueryFilters.Visitors;

#nullable enable

[assembly: InternalsVisibleTo("WebFeatures.QueryFilters.Tests")]
namespace WebFeatures.QueryFilters
{
    public static class QueryFiltersExtensions
    {
        public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> sourceQueryable, string query)
        {
            return (IQueryable<T>)ApplyQuery((IQueryable)sourceQueryable, query);
        }

        public static IQueryable ApplyQuery(this IQueryable sourceQueryable, string query)
        {
            var parser = new QueryFiltersParser(
                new CommonTokenStream(
                    new QueryFiltersLexer(
                        new AntlrInputStream(query))));

            var visitor = new QueryVisitor(sourceQueryable);
            var resultQueryable = parser.query().Accept(visitor);

            return resultQueryable;
        }
    }
}
