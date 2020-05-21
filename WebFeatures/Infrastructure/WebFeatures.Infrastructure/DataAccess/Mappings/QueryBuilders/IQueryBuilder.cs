using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Querying;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.QueryBuilders
{
    internal interface IQueryBuilder<TEntity, TQueries>
        where TEntity : Entity, new()
        where TQueries : Queries, new()
    {
        TQueries BuildQueries();
    }
}
