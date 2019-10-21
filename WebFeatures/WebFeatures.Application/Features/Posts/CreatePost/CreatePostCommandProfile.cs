using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommandProfile : Profile
    {
        public CreatePostCommandProfile()
        {
            CreateMap<CreatePostCommand, Post>(MemberList.Source);
        }
    }
}
