using AutoMapper;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Features.Products.GetReviews
{
    public class ReviewInfoDto : IHasMappings
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public UserRating Rating { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? AuthorPictureId { get; set; }
        public Guid ProductId { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<Review, ReviewInfoDto>(MemberList.Destination);
        }
    }
}
