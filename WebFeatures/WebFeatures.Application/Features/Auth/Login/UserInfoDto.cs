using AutoMapper;
using System.Linq;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class UserInfoDto : IHasMappings
    {
        public string Id { get; set; }
        public string[] Roles { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<User, UserInfoDto>(MemberList.Destination)
                .ForMember(x => x.Roles, y => y.Ignore())
                .AfterMap((s, d) =>
                {
                    d.Roles = s.UserRoles.Select(x => x.Role.Name).ToArray();
                });
        }
    }
}
