using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Auth.RegisterUser;
using WebFeatures.Application.Infrastructure.Exceptions;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Model;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, UserInfoDto>
    {
        private readonly IAsyncRepository<User> _userRepo;
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        private readonly IMapper _mapper;

        public LoginCommandHandler(
            IAsyncRepository<User> userRepo,
            IPasswordEncoder passwordEncoder,
            ILogger<RegisterUserCommandHandler> logger,
            IMapper mapper)
        {
            _userRepo = userRepo;
            _passwordEncoder = passwordEncoder;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserInfoDto> HandleAsync(LoginCommand request)
        {
            var user = await _userRepo.GetAll()
                .AsNoTracking()
                .SingleAsync(x => x.Email == request.Email);

            var password = _passwordEncoder.DecodePassword(user.PasswordHash);
            if (password != request.Password)
                throw new StatusCodeException("Wrong login or password");

            _logger.LogInformation($"{user.Email} logged in");

            return _mapper.Map<UserInfoDto>(user);
        }
    }
}
