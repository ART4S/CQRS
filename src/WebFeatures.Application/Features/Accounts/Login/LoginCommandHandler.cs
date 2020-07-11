using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Accounts.Login
{
	internal class LoginCommandHandler : IRequestHandler<LoginCommand, Guid>
	{
		private readonly IWriteDbContext _db;
		private readonly ILogger<LoginCommand> _logger;
		private readonly IPasswordHasher _passwordHasher;

		public LoginCommandHandler(
			IWriteDbContext db,
			IPasswordHasher passwordHasher,
			ILogger<LoginCommand> logger)
		{
			_db = db;
			_passwordHasher = passwordHasher;
			_logger = logger;
		}

		public async Task<Guid> HandleAsync(LoginCommand request, CancellationToken cancellationToken)
		{
			const string errorMessage = "Wrong login or password";

			User user = await _db.Users.GetByEmailAsync(request.Email) ?? throw new ValidationException(errorMessage);

			if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
				throw new ValidationException(errorMessage);

			_logger.LogInformation($"{user.Email} signed in");

			return user.Id;
		}
	}
}
