using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;

namespace WebFeatures.Application.Features.Registration.RegisterUser
{
    public class RegisterUserCommand : ICommand<Unit>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ContactDetailsEmail { get; set; }
        public string ContactDetailsPhoneNumber { get; set; }
    }
}
