using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.GetProductList
{
	internal class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IEnumerable<ProductListDto>>
	{
		private readonly IReadDbContext _db;

		public GetProductListQueryHandler(IReadDbContext db)
		{
			_db = db;
		}

		public Task<IEnumerable<ProductListDto>> HandleAsync(
			GetProductListQuery request,
			CancellationToken cancellationToken)
		{
			return _db.Products.GetListAsync();
		}
	}
}
