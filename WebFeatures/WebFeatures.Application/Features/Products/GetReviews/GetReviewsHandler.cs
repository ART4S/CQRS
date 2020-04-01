using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetReviews
{
    public class GetReviewsHandler : IRequestHandler<GetReviews, IQueryable<ReviewInfoDto>>
    {
        private readonly IWriteContext _db;
        private readonly IMapper _mapper;

        public GetReviewsHandler(IWriteContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public Task<IQueryable<ReviewInfoDto>> HandleAsync(GetReviews request, CancellationToken cancellationToken)
        {
            IQueryable<ReviewInfoDto> reviews = _db.Reviews
                .Where(x => x.ProductId == request.ProductId)
                .ProjectTo<ReviewInfoDto>(_mapper.ConfigurationProvider);

            return Task.FromResult(reviews);
        }
    }
}
