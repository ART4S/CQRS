using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Registration.RegisterUser
{
    public class RegisterUserCommandProfile : Profile
    {
        public RegisterUserCommandProfile()
        {
            CreateMap<User, RegisterUserCommand>(MemberList.Destination)
                .ForMember(x => x.Password, y => y.Ignore())
                .ForMember(x => x.ConfirmPassword, y => y.Ignore())
                .ReverseMap();
        }
    }
}
