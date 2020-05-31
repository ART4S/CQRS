using AutoMapper;
using System.Linq;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Auth.Dto
{
    public class UserInfoDto : IHasMappings
    {
        public string Id { get; set; }
        public string[] Roles { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<User, UserInfoDto>(MemberList.Destination)
                .ForMember(
                    dest => dest.Roles,
                    opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name)));
        }
    }
}
