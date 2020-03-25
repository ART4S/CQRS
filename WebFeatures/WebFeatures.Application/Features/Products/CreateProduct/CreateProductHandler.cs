using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Helpers;
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

            var product = new Product(
                request.Name, 
                request.Description, 
                request.ManufacturerId, 
                request.BrandId)
            {
                CategoryId = request.CategoryId
            };

            if (request.Picture != null)
            {
                product.Picture = new File(request.Picture.ReadBytes());
            }

            await _db.Products.AddAsync(product, cancellationToken);

            return product.Id;
        }
    }
}
