using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Features.Products.Requests.Queries;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;

namespace WebFeatures.Application.Features.Products.Handlers
{
    internal class ProductQueryHandler :
        IRequestHandler<GetProduct, ProductInfoDto>,
        IRequestHandler<GetProductList, IEnumerable<ProductListDto>>,
        IRequestHandler<GetProductComments, IEnumerable<ProductCommentInfoDto>>,
        IRequestHandler<GetProductReviews, IEnumerable<ProductReviewInfoDto>>
    {
        private readonly IReadDbContext _db;

        public ProductQueryHandler(IReadDbContext db)
        {
            _db = db;
        }

        public async Task<ProductInfoDto> HandleAsync(GetProduct request, CancellationToken cancellationToken)
        {
            ProductInfoDto product = await _db.Products.GetProductAsync(request.Id);

            if (product == null)
            {
                throw new ValidationException("Product doesn't exist");
            }

            return product;
        }

        public Task<IEnumerable<ProductListDto>> HandleAsync(GetProductList request, CancellationToken cancellationToken)
        {
            return _db.Products.GetListAsync();
        }

        public Task<IEnumerable<ProductCommentInfoDto>> HandleAsync(GetProductComments request, CancellationToken cancellationToken)
        {
            return _db.Products.GetCommentsAsync(request.Id);
        }

        public Task<IEnumerable<ProductReviewInfoDto>> HandleAsync(GetProductReviews request, CancellationToken cancellationToken)
        {
            return _db.Products.GetReviewsAsync(request.Id);
        }
    }
}