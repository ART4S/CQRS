using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Reading
{
    internal class ProductReadRepository : IProductReadRepository
    {
        public Task<IEnumerable<ProductCommentInfoDto>> GetCommentsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductListDto>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductInfoDto> GetProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductReviewInfoDto>> GetReviewsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
