using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Querying;

namespace WebFeatures.Infrastructure.DataAccess.Repositories
{
    internal class ProductCommentRepository : Repository<ProductComment, ProductCommentQueries>, IProductCommentRepository
    {
        public ProductCommentRepository(IDbConnection connection, ProductCommentQueries queries) : base(connection, queries)
        {
        }

        public Task<IEnumerable<ProductComment>> GetByProductAsync(Guid productId)
        {
            return Connection.QueryAsync<ProductComment>(Queries.GetByProduct, new { productId });
        }
    }
}
