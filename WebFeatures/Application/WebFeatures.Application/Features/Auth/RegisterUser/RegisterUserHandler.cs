﻿using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Auth.RegisterUser
{
    internal class RegisterUserHandler : IRequestHandler<RegisterUser, UserInfoDto>
    {
        private readonly IDbContext _db;
        private readonly IPasswordHasher _passwordEncoder;
        private readonly ILogger<RegisterUserHandler> _logger;
        private readonly IMapper _mapper;

        public RegisterUserHandler(
            IDbContext db,
            IPasswordHasher passwordEncoder,
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
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordEncoder.EncodePassword(request.Password)
            };

            await _db.Users.CreateAsync(user);

            _logger.LogInformation($"User {user.Name} registered with id: {user.Id}");

            return _mapper.Map<UserInfoDto>(user);
        }
    }
}