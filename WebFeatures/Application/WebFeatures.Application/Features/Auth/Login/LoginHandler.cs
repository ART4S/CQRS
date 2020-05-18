using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Auth.RegisterUser;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Auth.Login
{
    internal class LoginHandler : IRequestHandler<Login, UserInfoDto>
    {
        private readonly IDbContext _db;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<RegisterUserHandler> _logger;
        private readonly IMapper _mapper;

        public LoginHandler(
            IDbContext db,
            IPasswordHasher passwordHasher,
            ILogger<RegisterUserHandler> logger,
            IMapper mapper)
        {
            _db = db;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserInfoDto> HandleAsync(Login request, CancellationToken cancellationToken)
        {
            User user = await _db.Users.GetByEmailAsync(request.Email);

            if (user == null)
                throw new ApplicationValidationException("Wrong login or password");

            if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
                throw new ApplicationValidationException("Wrong login or password");

            _logger.LogInformation($"{user.Email} signed in");

            return _mapper.Map<UserInfoDto>(user);
        }
    }
}
