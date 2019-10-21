using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.GetBlogsInfo
{
    public class GetBlogInfosQueryProfile : Profile
    {
        public GetBlogInfosQueryProfile()
        {
            CreateMap<Blog, BlogInfoDto>(MemberList.Destination);
        }
    }
}
