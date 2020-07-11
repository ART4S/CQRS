using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.GetProduct
{
	internal class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductInfoDto>
	{
		private readonly IReadDbContext _db;

		public GetProductQueryHandler(IReadDbContext db)
		{
			_db = db;
		}

		public async Task<ProductInfoDto> HandleAsync(GetProductQuery request, CancellationToken cancellationToken)
		{
			return await _db.Products.GetProductAsync(request.Id) ?? throw new ValidationException("Product doesn't exist");
		}
	}
}
