using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Queries;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.QueryProviders
{
    internal interface IQueryProvider<TEntity, TQueries>
        where TEntity : BaseEntity
        where TQueries : IQueries<TEntity>
    {
        TQueries GetQueries();
    }
}
