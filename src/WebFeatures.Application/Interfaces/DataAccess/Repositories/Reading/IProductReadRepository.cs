using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.GetProduct;
using WebFeatures.Application.Features.Products.GetProductComments;
using WebFeatures.Application.Features.Products.GetProductList;
using WebFeatures.Application.Features.Products.GetProductReviews;

namespace WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories
{
    public interface IProductReadRepository
    {
        Task<ProductInfoDto> GetProductAsync(Guid id);
        Task<IEnumerable<ProductListDto>> GetListAsync();
        Task<IEnumerable<ProductCommentInfoDto>> GetCommentsAsync(Guid id);
        Task<IEnumerable<ProductReviewInfoDto>> GetReviewsAsync(Guid id);
    }
}