using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.GetProductComments
{
    internal class GetProductCommentsQueryHandler : IRequestHandler<GetProductCommentsQuery, IEnumerable<ProductCommentInfoDto>>
    {
        private readonly IReadDbContext _db;

        public GetProductCommentsQueryHandler(IReadDbContext db)
        {
            _db = db;
        }

        public Task<IEnumerable<ProductCommentInfoDto>> HandleAsync(GetProductCommentsQuery request, CancellationToken cancellationToken)
        {
            return _db.Products.GetCommentsAsync(request.Id);
        }
    }
}