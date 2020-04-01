using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditProduct, Empty>
    {
        private readonly IWriteContext _db;

        public EditProductHandler(IWriteContext db) => _db = db;

        public async Task<Empty> HandleAsync(EditProduct request, CancellationToken cancellationToken)
        {
            Product product = await _db.Products.FindAsync(request.Id);
            product.Name = product.Name;

            if (request.Price.HasValue)
            {
                product.SetPrice(request.Price.Value);
            }

            product.Description = request.Description;
            product.ManufacturerId = request.ManufacturerId;
            product.CategoryId = request.CategoryId;
            product.BrandId = request.BrandId;

            if (request.Picture != null)
            {
                product.Picture = new File(request.Picture);
            }

            return Empty.Value;
        }
    }
}
