using AutoMapper;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Products.GetProductComments
{
    public class ProductCommentInfoDto : IHasMappings
    {
        public Guid Id { get; set; }
        public Guid? ParentCommentId { get; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; }
        public DateTime CreatedAt { get; }
        public string Body { get; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<ProductComment, ProductCommentInfoDto>(MemberList.Destination);
        }
    }
}
