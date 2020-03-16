using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProduct, Guid>
    {
        private readonly IWebFeaturesDbContext _db;

        public CreateProductHandler(IWebFeaturesDbContext db) => _db = db;

        public async Task<Guid> HandleAsync(CreateProduct request, CancellationToken cancellationToken)
        {
            var manufacturer = await _db.Manufacturers.FindAsync(request.ManufacturerId);
            var brand = await _db.Brands.FindAsync(request.BrandId);

            var product = new Product(request.Name, request.Description, manufacturer, brand);

            if (request.CategoryId.HasValue)
            {
                var category = await _db.Categories.FindAsync(request.CategoryId);
                product.SetCategory(category);
            }

            await _db.Products.AddAsync(product, cancellationToken);

            return product.Id;
        }
    }
}
