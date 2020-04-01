using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Auth.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, UserInfoDto>
    {
        private readonly IWriteContext _db;
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly ILogger<RegisterUserHandler> _logger;
        private readonly IMapper _mapper;

        public RegisterUserHandler(
            IWriteContext db,
            IPasswordEncoder passwordEncoder,
            ILogger<RegisterUserHandler> logger,
            IMapper mapper)
        {
            _db = db;
            _passwordEncoder = passwordEncoder;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserInfoDto> HandleAsync(RegisterUser request, CancellationToken cancellationToken)
        {
            string passwordHash = _passwordEncoder.EncodePassword(request.Password);
            var user = new User(request.Name, request.Email, passwordHash);

            await _db.Users.AddAsync(user, cancellationToken);

            _logger.LogInformation($"User {user.Name} registered with id: {user.Id}");

            return _mapper.Map<UserInfoDto>(user);
        }
    }
}
