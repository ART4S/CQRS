using System;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal class ProductReviewQueryBuilder : QueryBuilder<ProductReview>
    {
        public ProductReviewQueryBuilder(EntityMap<ProductReview> entityMap) : base(entityMap)
        {
        }

        public SqlQuery BuildGetByProduct(Guid productId)
        {
            PropertyMap productIdMap = EntityMap.GetPropertyMap(x => x.AuthorId);

            string query =
                $"SELECT * FROM {EntityMap.Table.Schema}.{EntityMap.Table.Name}" +
                $"WHERE {productIdMap.Field} = @{nameof(productId)}";

            return new SqlQuery(query, new { productId });
        }
    }
}