using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductComments
{
    internal class GetProductCommentsHandler : IRequestHandler<GetProductComments, IQueryable<CommentInfoDto>>
    {
        private readonly IDbContext _db;
        private readonly IMapper _mapper;

        public GetProductCommentsHandler(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public Task<IQueryable<CommentInfoDto>> HandleAsync(GetProductComments request, CancellationToken cancellationToken)
        {
            IQueryable<CommentInfoDto> comments = _db.UserComments
                .Where(x => x.ProductId == request.ProductId)
                .AsQueryable()
                .ProjectTo<CommentInfoDto>(_mapper.ConfigurationProvider);

            return Task.FromResult(comments);
        }
    }
}
