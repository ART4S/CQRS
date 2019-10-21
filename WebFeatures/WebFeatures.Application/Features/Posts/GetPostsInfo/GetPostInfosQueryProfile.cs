using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostsInfo
{
    public class GetPostInfosQueryProfile : Profile
    {
        public GetPostInfosQueryProfile()
        {
            CreateMap<Post, PostInfoDto>(MemberList.Destination);
        }
    }
}
