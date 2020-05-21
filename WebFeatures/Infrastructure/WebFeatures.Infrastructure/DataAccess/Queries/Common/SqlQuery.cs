namespace WebFeatures.Infrastructure.DataAccess.Queries.Common
{
    internal class SqlQuery
    {
        public string Query { get; }
        public object Param { get; }

        public SqlQuery(string query, object param = null)
        {
            Query = query;
            Param = param;
        }
    }
}
