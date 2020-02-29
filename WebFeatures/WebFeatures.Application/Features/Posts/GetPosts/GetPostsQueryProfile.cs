using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPosts
{
    public class GetPostsQueryProfile : Profile
    {
        public GetPostsQueryProfile()
        {
            CreateMap<Post, PostInfoDto>(MemberList.Destination);
        }
    }
}
