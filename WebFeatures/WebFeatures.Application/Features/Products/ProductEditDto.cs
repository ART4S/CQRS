using System;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    public class ProductEditDto
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }
}
