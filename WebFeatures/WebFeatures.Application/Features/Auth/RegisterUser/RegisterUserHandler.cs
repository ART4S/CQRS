using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Auth.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, Empty>
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

        public async Task<Empty> HandleAsync(RegisterUser request, CancellationToken cancellationToken)
        {
            string passwordHash = _passwordEncoder.EncodePassword(request.Password);
            var user = new User(request.Name, request.Email, passwordHash);

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            _logger.LogInformation($"User {user.Name} registered with id: {user.Id}");

            return Empty.Value;
        }
    }
}
