using AutoMapper;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.GetBlogs
{
    public class BlogInfoDto : IHasMappings
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<Blog, BlogInfoDto>(MemberList.Destination);
        }
    }
}
