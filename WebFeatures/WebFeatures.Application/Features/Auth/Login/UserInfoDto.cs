using AutoMapper;
using System.Linq;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Model;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class UserInfoDto : IHasMappings
    {
        public string Id { get; set; }
        public string[] Roles { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<User, UserInfoDto>(MemberList.Destination)
                .ForMember(x => Roles, y => y.MapFrom(z => z.Roles.Select(x => x.Name)));
        }
    }
}
