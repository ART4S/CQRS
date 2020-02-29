using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.GetBlogs
{
    public class GetBlogsQueryProfile : Profile
    {
        public GetBlogsQueryProfile()
        {
            CreateMap<Blog, BlogInfoDto>(MemberList.Destination);
        }
    }
}
