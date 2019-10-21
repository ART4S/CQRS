using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Registration.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Unit>
    {
        private readonly IDataProtector _protector;
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IDataProtectionProvider protectionProvider, IAppContext context, IMapper mapper)
        {
            _protector = protectionProvider.CreateProtector("UserPassword");
            _context = context;
            _mapper = mapper;
        }

        public Unit Handle(RegisterUserCommand input)
        {
            var user = _mapper.Map<User>(input);
            user.PasswordHash = _protector.Protect(input.Password);

            _context.Add(user);

            return Unit.Value;
        }
    }
}
