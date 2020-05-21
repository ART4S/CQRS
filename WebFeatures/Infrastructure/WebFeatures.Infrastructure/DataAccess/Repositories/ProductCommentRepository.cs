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
    internal class ProductCommentRepository : Repository<ProductComment, ProductCommentQueryBuilder>, IProductCommentRepository
    {
        public ProductCommentRepository(
            IDbConnection connection,
            ProductCommentQueryBuilder queryBuilder) : base(connection, queryBuilder)
        {
        }

        public Task<IEnumerable<ProductComment>> GetByProductAsync(Guid productId)
        {
            SqlQuery sql = QueryBuilder.BuildGetByProduct(productId);

            return Connection.QueryAsync<ProductComment>(sql.Query, sql.Param);
        }
    }
}