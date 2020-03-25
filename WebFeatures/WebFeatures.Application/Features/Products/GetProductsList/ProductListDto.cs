using AutoMapper;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Products.GetProductsList
{
    public class ProductListDto : IHasMappings
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public Guid? PictureId { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<Product, ProductListDto>(MemberList.Destination);
        }
    }
}
