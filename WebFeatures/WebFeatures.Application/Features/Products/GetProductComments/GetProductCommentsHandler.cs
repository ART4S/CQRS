using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductComments
{
    public class GetProductCommentsHandler : IRequestHandler<GetProductComments, IQueryable<CommentInfoDto>>
    {
        private readonly IWebFeaturesDbContext _db;
        private readonly IMapper _mapper;

        public GetProductCommentsHandler(IWebFeaturesDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public Task<IQueryable<CommentInfoDto>> HandleAsync(GetProductComments request, CancellationToken cancellationToken)
        {
            IQueryable<CommentInfoDto> comments = _db.UserComments
                .Where(x => x.ProductId == request.ProductId)
                .ProjectTo<CommentInfoDto>(_mapper.ConfigurationProvider);

            return Task.FromResult(comments);
        }
    }
}
