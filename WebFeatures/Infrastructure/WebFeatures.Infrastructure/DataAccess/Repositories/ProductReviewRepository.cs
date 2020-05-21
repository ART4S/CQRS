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
    internal class ProductReviewRepository : Repository<ProductReview, ProductReviewQueries>, IProductReviewRepository
    {
        public ProductReviewRepository(IDbConnection connection, ProductReviewQueries queries) : base(connection, queries)
        {
        }

        public Task<IEnumerable<ProductReview>> GetByProductAsync(Guid productId)
        {
            return Connection.QueryAsync<ProductReview>(Queries.GetByProduct, new { productId });
        }
    }
}
