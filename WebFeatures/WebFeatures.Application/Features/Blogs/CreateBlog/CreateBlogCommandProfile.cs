using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommandProfile : Profile
    {
        public CreateBlogCommandProfile()
        {
            CreateMap<CreateBlogCommand, Blog>(MemberList.Source);
        }
    }
}
