using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetReviews
{
    public class GetReviewsHandler : IRequestHandler<GetReviews, IQueryable<ReviewInfoDto>>
    {
        private readonly IReadContext _db;
        private readonly IMapper _mapper;

        public GetReviewsHandler(IReadContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<IQueryable<ReviewInfoDto>> HandleAsync(GetReviews request, CancellationToken cancellationToken)
        {
            IQueryable<ReviewInfoDto> reviews =
                (await _db.GetAllAsync<Review>())
                .Where(x => x.ProductId == request.ProductId)
                .AsQueryable()
                .ProjectTo<ReviewInfoDto>(_mapper.ConfigurationProvider);

            return reviews;
        }
    }
}
