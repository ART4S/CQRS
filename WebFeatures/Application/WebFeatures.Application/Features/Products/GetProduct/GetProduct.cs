using System;
using WebFeatures.Application.Features.Products.GetProduct;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.GetProduct
{
    public class GetProduct : IQuery<ProductInfoDto>
    {
        public Guid Id { get; set; }
    }
}
