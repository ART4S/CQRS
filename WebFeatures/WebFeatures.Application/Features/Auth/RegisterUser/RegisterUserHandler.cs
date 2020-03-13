using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Model;
using WebFeatures.RequestHandling;

namespace WebFeatures.Application.Features.Auth.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, Unit>
    {
        private readonly IAsyncRepository<User> _userRepo;
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly ILogger<RegisterUserHandler> _logger;

        public RegisterUserHandler(
            IAsyncRepository<User> userRepo,
            IPasswordEncoder passwordEncoder,
            ILogger<RegisterUserHandler> logger)
        {
            _userRepo = userRepo;
            _passwordEncoder = passwordEncoder;
            _logger = logger;
        }

        public async Task<Unit> HandleAsync(RegisterUser request, CancellationToken cancellationToken)
        {
            string passwordHash = _passwordEncoder.EncodePassword(request.Password);
            var user = new User(request.Name, request.Email, passwordHash);

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            _logger.LogInformation($"{user.Name} registered. Id: {user.Id}");

            return Unit.Value;
        }
    }
}
