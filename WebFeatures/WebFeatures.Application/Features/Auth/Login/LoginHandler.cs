using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Auth.RegisterUser;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Model;
using WebFeatures.RequestHandling;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class LoginHandler : IRequestHandler<Login, UserInfoDto>
    {
        private readonly IAsyncRepository<User> _userRepo;
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly ILogger<RegisterUserHandler> _logger;
        private readonly IMapper _mapper;

        public LoginHandler(
            IAsyncRepository<User> userRepo,
            IPasswordEncoder passwordEncoder,
            ILogger<RegisterUserHandler> logger,
            IMapper mapper)
        {
            _userRepo = userRepo;
            _passwordEncoder = passwordEncoder;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserInfoDto> HandleAsync(Login request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetAll()
                .AsNoTracking()
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .SingleAsync(x => x.Email == request.Email, cancellationToken);

            var password = _passwordEncoder.DecodePassword(user.PasswordHash);
            if (password != request.Password)
                throw new StatusCodeException("Wrong login or password");

            _logger.LogInformation($"{user.Email} logged in");

            return _mapper.Map<UserInfoDto>(user);
        }
    }
}
