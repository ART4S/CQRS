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
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public UserRating Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<Review, ReviewInfoDto>(MemberList.Destination);
        }
    }
}
