using AutoMapper;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Products.Requests.GetProductComments
{
    public class CommentInfoDto : IHasMappings
    {
        public Guid Id { get; set; }
        public Guid? ParentCommentId { get; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; }
        public DateTime CreatedAt { get; }
        public string Body { get; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<UserComment, CommentInfoDto>(MemberList.Destination);
        }
    }
}
