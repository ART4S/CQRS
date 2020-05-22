namespace WebFeatures.Infrastructure.DataAccess.Queries.Common
{
    internal class SqlQuery
    {
        public string Query { get; }
        public object Param { get; }
        public string SplitOn { get; }

        public SqlQuery(string query, object param = null, string splitOn = "Id")
        {
            Query = query;
            Param = param;
            SplitOn = splitOn;
        }
    }
}
