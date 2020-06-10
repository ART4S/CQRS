using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Accounts.Requests.Commands;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Accounts.Handlers
{
    internal class AccountCommandHandler :
        IRequestHandler<Register, Guid>,
        IRequestHandler<Login, Guid>
    {
        private readonly IWriteDbContext _db;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILoggerFactory _loggerFactory;

        public AccountCommandHandler(
            IWriteDbContext db,
            IPasswordHasher passwordHasher,
            ILoggerFactory loggerFactory)
        {
            _db = db;
            _passwordHasher = passwordHasher;
            _loggerFactory = loggerFactory;
        }

        public async Task<Guid> HandleAsync(Register request, CancellationToken cancellationToken)
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

            return user.Id;
        }

        public async Task<Guid> HandleAsync(Login request, CancellationToken cancellationToken)
        {
            const string errorMessage = "Неверный логин или пароль";

            User user = await _db.Users.GetByEmailAsync(request.Email) ?? throw new ValidationException(errorMessage);

            if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                throw new ValidationException(errorMessage);
            }

            _loggerFactory.CreateLogger<Login>()
                .LogInformation($"{user.Email} signed in");

            return user.Id;
        }
    }
}