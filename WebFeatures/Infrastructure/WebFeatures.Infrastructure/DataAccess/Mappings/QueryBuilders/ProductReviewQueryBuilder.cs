using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Querying;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.QueryBuilders
{
    internal class ProductReviewQueryBuilder : QueryBuilder<ProductReview, ProductReviewQueries>
    {
        public ProductReviewQueryBuilder(IEntityProfile profile) : base(profile)
        {
        }

        protected override ProductReviewQueries BuildQueries(EntityMap<ProductReview> entity)
        {
            ProductReviewQueries queries = base.BuildQueries(entity);

            queries.GetByProduct = BuildGetByProduct(entity);

            return queries;
        }

        private string BuildGetByProduct(EntityMap<ProductReview> entity)
        {
            PropertyMap productId = entity.GetPropertyMap(x => x.AuthorId);

            string sql =
                $"SELECT * FROM {entity.Table.Schema}.{entity.Table.Name}" +
                $"WHERE {productId.Field} = @{productId.Property}";

            return sql;
        }
    }
}
