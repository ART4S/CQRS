using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Accounts.Dto;
using WebFeatures.Application.Features.Accounts.Requests.Commands;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Accounts.Handlers
{
    internal class AccountCommandHandler :
        IRequestHandler<Register, UserInfoDto>,
        IRequestHandler<Login, UserInfoDto>
    {
        private readonly IWriteDbContext _db;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IMapper _mapper;

        public AccountCommandHandler(
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

        public async Task<UserInfoDto> HandleAsync(Register request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.ComputeHash(request.Password)
            };

            await _db.Users.CreateAsync(user);

            _loggerFactory.CreateLogger<Register>()
                .LogInformation($"User {user.Name} registered with id: {user.Id}");

            return _mapper.Map<UserInfoDto>(user);
        }

        public async Task<UserInfoDto> HandleAsync(Login request, CancellationToken cancellationToken)
        {
            const string errorMessage = "Неверный логин или пароль";

            User user = await _db.Users.GetByEmailAsync(request.Email) ?? throw new ValidationException(errorMessage);

            if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                throw new ValidationException(errorMessage);
            }

            _loggerFactory.CreateLogger<Login>()
                .LogInformation($"{user.Email} signed in");

            return _mapper.Map<UserInfoDto>(user);
        }
    }
}