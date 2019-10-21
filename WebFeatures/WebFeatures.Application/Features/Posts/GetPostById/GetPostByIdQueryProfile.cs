using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class GetPostByIdQueryProfile : Profile
    {
        public GetPostByIdQueryProfile()
        {
            CreateMap<Post, PostEditDto>(MemberList.Destination);
        }
    }
}
