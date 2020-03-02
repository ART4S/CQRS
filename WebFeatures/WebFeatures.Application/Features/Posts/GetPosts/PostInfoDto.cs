using AutoMapper;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPosts
{
    public class PostInfoDto : IHasMappings
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int BlogId { get; set; }
        public string BlogAuthorName { get; set; }
        public string BlogTitle { get; set; }
        public string Title { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<Post, PostInfoDto>(MemberList.Destination);
        }
    }
}
