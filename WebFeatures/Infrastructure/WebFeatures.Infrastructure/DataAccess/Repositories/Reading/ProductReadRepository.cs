using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;
using WebFeatures.Infrastructure.DataAccess.Constants;

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
                sql: ViewNames.Products.GET_PRODUCTS_LIST,
                commandType: CommandType.TableDirect);
        }

        public Task<IEnumerable<ProductCommentInfoDto>> GetCommentsAsync(Guid id)
        {
            string sql = $"SELECT * FROM {ViewNames.Products.GET_PRODUCT_COMMENTS} WHERE productid = @id";

            return Connection.QueryAsync<ProductCommentInfoDto>(sql, new { id });
        }

        public Task<IEnumerable<ProductReviewInfoDto>> GetReviewsAsync(Guid id)
        {
            string sql = $"SELECT * FROM {ViewNames.Products.GET_PRODUCT_REVIEWS} WHERE productid = @id";

            return Connection.QueryAsync<ProductReviewInfoDto>(sql, new { id });
        }
    }
}