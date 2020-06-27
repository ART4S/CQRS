using Bogus;
using WebFeatures.Application.Features.Accounts.Login;

namespace WebFeatures.Application.Tests.Common.Stubs.Requests.Accounts
{
    internal class LoginStub : Faker<LoginCommand>
    {
        public LoginStub()
        {
            StrictMode(true);

            RuleFor(x => x.Email, x => x.Internet.Email());
            RuleFor(x => x.Password, x => x.Internet.Password());
        }
    }
}
