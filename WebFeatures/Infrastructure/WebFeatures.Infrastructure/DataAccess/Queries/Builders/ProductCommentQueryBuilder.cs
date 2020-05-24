using System;
using System.Text;
using WebFeatures.Domian.Entities;
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
            var entity = Profile.GetMap<ProductComment>();
            var query = new StringBuilder();

            query.AppendFormat("SELECT * FROM {0}\n", entity.Table.NameWithSchema());
            query.AppendFormat("WHERE {0} = @productId\n", entity.Column(x => x.AuthorId));

            return new SqlQuery(query.ToString(), new { productId });
        }
    }
}