using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces.DataAccess.Repositories
{
    public interface IProductCommentRepository : IAsyncRepository<ProductComment>
    {
        Task<IEnumerable<ProductComment>> GetByProductAsync(Guid productId);
    }
}
