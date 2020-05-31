using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;

namespace WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories
{
    public interface IProductReadRepository
    {
        Task<ProductInfoDto> GetProductAsync(Guid id);
        Task<IEnumerable<ProductCommentInfoDto>> GetCommentsAsync(Guid id);
        Task<IEnumerable<ProductReviewInfoDto>> GetReviewsAsync(Guid id);
        Task<IEnumerable<ProductListDto>> GetListAsync();
    }
}
