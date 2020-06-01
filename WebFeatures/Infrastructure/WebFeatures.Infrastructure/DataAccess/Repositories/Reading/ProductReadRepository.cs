using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Reading
{
    internal class ProductReadRepository : ReadRepository, IProductReadRepository
    {
        public ProductReadRepository(IDbConnection connection) : base(connection)
        {
        }

        public Task<ProductInfoDto> GetProductAsync(Guid id)
        {
            return Connection.QuerySingleOrDefaultAsync<ProductInfoDto>(
                sql: "get_product",
                param: new { product_id = id },
                commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<ProductListDto>> GetListAsync()
        {
            return Connection.QueryAsync<ProductListDto>(
                sql: "get_products_list",
                commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<ProductCommentInfoDto>> GetCommentsAsync(Guid id)
        {
            return Connection.QueryAsync<ProductCommentInfoDto>(
                sql: "get_product_comments",
                param: new { product_id = id },
                commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<ProductReviewInfoDto>> GetReviewsAsync(Guid id)
        {
            return Connection.QueryAsync<ProductReviewInfoDto>(
                sql: "get_product_reviews",
                param: new { product_id = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}