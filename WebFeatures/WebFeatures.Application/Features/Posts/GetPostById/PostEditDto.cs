using AutoMapper;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class PostEditDto : IHasMappings
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<Post, PostEditDto>(MemberList.Destination);
        }
    }
}
