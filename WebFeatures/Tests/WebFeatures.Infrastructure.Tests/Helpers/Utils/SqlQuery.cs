using WebFeatures.Common;

namespace WebFeatures.Infrastructure.Tests.Helpers.Utils
{
    internal class SqlQuery
    {
        public string Query { get; }
        public object Param { get; }

        public SqlQuery(string query, object param = null)
        {
            Guard.ThrowIfNull(query, nameof(query));

            Query = query;
            Param = param;
        }
    }
}
