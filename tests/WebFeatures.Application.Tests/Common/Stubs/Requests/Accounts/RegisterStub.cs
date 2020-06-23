using Bogus;
using WebFeatures.Application.Features.Accounts.Requests.Commands;

namespace WebFeatures.Application.Tests.Common.Stubs.Requests.Accounts
{
    internal class RegisterStub : Faker<Register>
    {
        public RegisterStub()
        {
            StrictMode(true);

            RuleFor(x => x.Name, x => x.Internet.UserName());
            RuleFor(x => x.Email, x => x.Internet.Email());
            RuleFor(x => x.Password, x => x.Internet.Password());
        }
    }
}