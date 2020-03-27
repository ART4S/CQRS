using AutoMapper;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Products.GetReviews
{
    public class ReviewInfoDto : IHasMappings
    {
        public decimal Score { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<Review, ReviewInfoDto>(MemberList.Destination)
                .ForMember(s => s.Score, o => o.Ignore());
        }
    }
}
