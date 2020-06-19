using Bogus;
using WebFeatures.Application.Features.Accounts.Requests.Commands;

namespace WebFeatures.Application.Tests.Common.Factories.Requests.Accounts
{
    internal static class RegisterFactory
    {
        public static Register Get()
        {
            var register = new Faker<Register>()
                .StrictMode(true)
                .RuleFor(x => x.Name, x => x.Internet.UserName())
                .RuleFor(x => x.Email, x => x.Internet.Email())
                .RuleFor(x => x.Password, x => x.Internet.Password());

            return register;
        }
    }
}
