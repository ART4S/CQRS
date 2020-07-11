using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Accounts;

namespace WebFeatures.Application.Features.Accounts.Register
{
	internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
	{
		private readonly IWriteDbContext _db;
		private readonly ILogger<RegisterCommand> _logger;
		private readonly IPasswordHasher _passwordHasher;

		public RegisterCommandHandler(
			IWriteDbContext db,
			IPasswordHasher passwordHasher,
			ILogger<RegisterCommand> logger)
		{
			_db = db;
			_passwordHasher = passwordHasher;
			_logger = logger;
		}

		public async Task<Guid> HandleAsync(RegisterCommand request, CancellationToken cancellationToken)
		{
			string hash = _passwordHasher.ComputeHash(request.Password);

			User user = new User
			{
				Name = request.Name,
				Email = request.Email,
				PasswordHash = hash
			};

			await _db.Users.CreateAsync(user);

			Role role = await _db.Roles.GetByNameAsync(AuthorizationConstants.Roles.Users)
			         ?? throw new InvalidOperationException("Cannot find role for new user");

			await _db.UserRoles.CreateAsync(new UserRole
			{
				UserId = user.Id,
				RoleId = role.Id
			});

			_logger.LogInformation($"User {user.Name} registered with id: {user.Id}");

			return user.Id;
		}
	}
}
