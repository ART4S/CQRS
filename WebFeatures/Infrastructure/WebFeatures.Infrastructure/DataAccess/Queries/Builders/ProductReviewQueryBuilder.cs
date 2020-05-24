using System;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;
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
            IEntityMap<ProductReview> review = Profile.GetMap<ProductReview>();

            string query = string.Format(
                @"SELECT * FROM {0} 
                  WHERE {1} = @productId",
                review.Table.NameWithSchema(),
                review.Column(x => x.AuthorId));

            return new SqlQuery(query, new { productId });
        }
    }
}