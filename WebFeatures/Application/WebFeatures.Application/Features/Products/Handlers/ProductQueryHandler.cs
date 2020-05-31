using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Features.Products.Requests.Queries;
using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.Handlers
{
    internal class ProductQueryHandler :
        IRequestHandler<GetProductsList, IEnumerable<ProductListDto>>,
        IRequestHandler<GetProduct, ProductInfoDto>,
        IRequestHandler<GetProductComments, IEnumerable<ProductCommentInfoDto>>,
        IRequestHandler<GetProductReviews, IEnumerable<ProductReviewInfoDto>>
    {
        private readonly IProductReadRepository _repo;

        public ProductQueryHandler(IProductReadRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ProductListDto>> HandleAsync(GetProductsList request, CancellationToken cancellationToken)
        {
            return _repo.GetListAsync();
        }

        public Task<ProductInfoDto> HandleAsync(GetProduct request, CancellationToken cancellationToken)
        {
            return _repo.GetProductAsync(request.Id);
        }

        public Task<IEnumerable<ProductCommentInfoDto>> HandleAsync(GetProductComments request, CancellationToken cancellationToken)
        {
            return _repo.GetCommentsAsync(request.ProductId);
        }

        public Task<IEnumerable<ProductReviewInfoDto>> HandleAsync(GetProductReviews request, CancellationToken cancellationToken)
        {
            return _repo.GetReviewsAsync(request.ProductId);
        }
    }
}