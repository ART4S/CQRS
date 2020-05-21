using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Repositories
{
    internal class ProductReviewRepository : Repository<ProductReview, ProductReviewQueryBuilder>, IProductReviewRepository
    {
        public ProductReviewRepository(
            IDbConnection connection,
            ProductReviewQueryBuilder queryBuilder) : base(connection, queryBuilder)
        {
        }

        public Task<IEnumerable<ProductReview>> GetByProductAsync(Guid productId)
        {
            SqlQuery sql = QueryBuilder.BuildGetByProduct(productId);

            return Connection.QueryAsync<ProductReview>(sql.Query, sql.Param);
        }
    }
}