using System;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Helpers;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal class ProductReviewQueryBuilder : QueryBuilder<ProductReview>
    {
        public ProductReviewQueryBuilder(IEntityProfile profile) : base(profile)
        {
        }

        public SqlQuery BuildGetByProduct(Guid productId)
        {
            string query =
                $"SELECT * FROM {EntityMap.Table.NameWithSchema()}" +
                $"WHERE {EntityMap.Field(x => x.AuthorId)} = @{nameof(productId)}";

            return new SqlQuery(query, new { productId });
        }
    }
}