using WebFeatures.Domian.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Queries
{
    internal interface IQueries<TEntity> where TEntity : BaseEntity
    {
        string Get { get; set; }
        string GetAll { get; set; }
        string Exists { get; set; }
        string Delete { get; set; }
        string Create { get; set; }
        string Update { get; set; }
    }
}
