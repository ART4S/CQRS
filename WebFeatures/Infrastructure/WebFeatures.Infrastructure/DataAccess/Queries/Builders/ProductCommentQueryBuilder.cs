using System;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal class ProductCommentQueryBuilder : QueryBuilder<ProductComment>
    {
        public ProductCommentQueryBuilder(IEntityProfile profile) : base(profile)
        {
        }

        public SqlQuery BuildGetByProduct(Guid productId)
        {
            IEntityMap<ProductComment> comment = Profile.GetMap<ProductComment>();

            string query = string.Format(
                @"SELECT * FROM {0} 
                  WHERE {1} = @productId",
                comment.Table.NameWithSchema(),
                comment.Column(x => x.AuthorId));

            return new SqlQuery(query, new { productId });
        }
    }
}