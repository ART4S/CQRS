using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductComments
{
    internal class GetProductCommentsHandler : IRequestHandler<GetProductComments, IEnumerable<ProductCommentInfoDto>>
    {
        private readonly IDbContext _db;
        private readonly IMapper _mapper;

        public GetProductCommentsHandler(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductCommentInfoDto>> HandleAsync(GetProductComments request, CancellationToken cancellationToken)
        {
            IEnumerable<ProductCommentInfoDto> comments =
                (await _db.ProductComments.GetByProductAsync(request.ProductId))
                .Select(x => _mapper.Map<ProductCommentInfoDto>(x));

            return comments;
        }
    }
}
