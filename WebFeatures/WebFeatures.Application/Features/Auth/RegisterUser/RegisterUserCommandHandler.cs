using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Model;

namespace WebFeatures.Application.Features.Auth.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterCustomerCommand, Unit>
    {
        private readonly IAsyncRepository<User> _userRepo;
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(
            IAsyncRepository<User> userRepo,
            IPasswordEncoder passwordEncoder,
            ILogger<RegisterUserCommandHandler> logger)
        {
            _userRepo = userRepo;
            _passwordEncoder = passwordEncoder;
            _logger = logger;
        }

        public async Task<Unit> HandleAsync(RegisterCustomerCommand request)
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
