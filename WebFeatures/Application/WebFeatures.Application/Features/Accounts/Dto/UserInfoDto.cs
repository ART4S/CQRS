using AutoMapper;
using System.Linq;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Accounts.Dto
{
    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public class UserInfoDto : IHasMappings
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
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