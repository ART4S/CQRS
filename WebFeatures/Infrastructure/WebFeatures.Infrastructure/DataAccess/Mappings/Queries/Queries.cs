using WebFeatures.Domian.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Queries
{
    internal class Queries<TEntity> : IQueries<TEntity>
        where TEntity : BaseEntity
    {
        public string GetAll { get; set; }
        public string Get { get; set; }
        public string Create { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
        public string Exists { get; set; }
    }
}
