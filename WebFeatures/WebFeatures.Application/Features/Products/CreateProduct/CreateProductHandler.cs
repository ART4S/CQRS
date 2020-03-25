using System;
using System.IO;
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
            Picture picture;
            using (var ms = new MemoryStream())
            {
                request.Picture.CopyTo(ms);
                picture = new Picture(ms.ToArray());
            }

            var product = new Product(
                request.Product.Name, 
                request.Product.Description, 
                request.Product.ManufacturerId, 
                request.Product.BrandId)
            {
                CategoryId = request.Product.CategoryId,
                Picture = picture
            };

            await _db.Products.AddAsync(product, cancellationToken);

            return product.Id;
        }
    }
}
