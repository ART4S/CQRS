using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces.DataAccess.Repositories
{
    public interface IProductReviewRepository : IAsyncRepository<ProductReview>
    {
        Task<IEnumerable<ProductReview>> GetByProductAsync(Guid productId);
    }
}
