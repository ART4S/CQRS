using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Querying;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.QueryBuilders
{
    internal class ProductCommentQueryBuilder : QueryBuilder<ProductComment, ProductCommentQueries>
    {
        public ProductCommentQueryBuilder(IEntityProfile profile) : base(profile)
        {
        }

        protected override ProductCommentQueries BuildQueries(EntityMap<ProductComment> entity)
        {
            ProductCommentQueries queries = base.BuildQueries(entity);

            queries.GetByProduct = BuildGetByProduct(entity);

            return queries;
        }

        private string BuildGetByProduct(EntityMap<ProductComment> entity)
        {
            PropertyMap productId = entity.GetPropertyMap(x => x.AuthorId);

            string sql =
                $"SELECT * FROM {entity.Table.Schema}.{entity.Table.Name}" +
                $"WHERE {productId.Field} = @{productId.Property}";

            return sql;
        }
    }
}
