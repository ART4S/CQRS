using System;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.Requests.Queries
{
    public class GetProduct : IQuery<ProductInfoDto>
    {
        public Guid Id { get; set; }
    }
}
