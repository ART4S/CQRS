using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.UpdatePost
{
    public class UpdatePostCommandProfile : Profile
    {
        public UpdatePostCommandProfile()
        {
            CreateMap<UpdatePostCommand, Post>(MemberList.Source);
        }
    }
}
