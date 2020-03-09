using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;

namespace WebFeatures.Application.Features.Auth.RegisterUser
{
    public class RegisterCustomerCommand : ICommand<Unit>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public class Validator : AbstractValidator<RegisterCustomerCommand>
        {
            public Validator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
