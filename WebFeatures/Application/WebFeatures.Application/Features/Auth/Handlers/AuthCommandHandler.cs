using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Auth.Dto;
using WebFeatures.Application.Features.Auth.Requests.Commands;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Auth.Handlers
{
    internal class AuthCommandHandler :
        IRequestHandler<RegisterUser, UserInfoDto>,
        IRequestHandler<Login, UserInfoDto>
    {
        private readonly IWriteDbContext _db;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IMapper _mapper;

        public AuthCommandHandler(
            IWriteDbContext db,
            IPasswordHasher passwordHasher,
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            _db = db;
            _passwordHasher = passwordHasher;
            _loggerFactory = loggerFactory;
            _mapper = mapper;
        }

        public async Task<UserInfoDto> HandleAsync(RegisterUser request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.ComputeHash(request.Password)
            };

            await _db.Users.CreateAsync(user);

            _loggerFactory.CreateLogger<RegisterUser>()
                .LogInformation($"User {user.Name} registered with id: {user.Id}");

            return _mapper.Map<UserInfoDto>(user);
        }

        public async Task<UserInfoDto> HandleAsync(Login request, CancellationToken cancellationToken)
        {
            User user = await _db.Users.GetByEmailAsync(request.Email);

            if (user == null)
                throw new ApplicationValidationException("Wrong login or password");

            if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
                throw new ApplicationValidationException("Wrong login or password");

            _loggerFactory.CreateLogger<Login>()
                .LogInformation($"{user.Email} signed in");

            return _mapper.Map<UserInfoDto>(user);
        }
    }
}