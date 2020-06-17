using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;
using WebFeatures.Infrastructure.DataAccess.Constants;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Reading
{
    internal class ProductReadRepository : ReadRepository, IProductReadRepository
    {
        public ProductReadRepository(IDbConnection connection, IDbExecutor executor) : base(connection, executor)
        {
        }

        public Task<ProductInfoDto> GetProductAsync(Guid id)
        {
            return Executor.QuerySingleOrDefaultAsync<ProductInfoDto>(
                connection: Connection,
                sql: "get_product",
                param: new { product_id = id },
                commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<ProductListDto>> GetListAsync()
        {
            return Executor.QueryAsync<ProductListDto>(
                connection: Connection,
                sql: ViewNames.GET_PRODUCTS_LIST,
                commandType: CommandType.TableDirect);
        }

        public Task<IEnumerable<ProductCommentInfoDto>> GetCommentsAsync(Guid id)
        {
            return Executor.QueryAsync<ProductCommentInfoDto>(
                connection: Connection,
                sql: "get_product_comments",
                param: new { product_id = id },
                commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<ProductReviewInfoDto>> GetReviewsAsync(Guid id)
        {
            return Executor.QueryAsync<ProductReviewInfoDto>(
                connection: Connection,
                sql: "get_product_reviews",
                param: new { product_id = id },
                commandType: CommandType.StoredProcedure);
        }
    }
}