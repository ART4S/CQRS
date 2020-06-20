using Bogus;
using WebFeatures.Application.Features.Accounts.Requests.Commands;

namespace WebFeatures.Application.Tests.Common.Factories.Features
{
    internal static class AccountsFactory
    {
        public static Register Register()
        {
            var register = new Faker<Register>()
                .RuleFor(x => x.Name, x => x.Internet.UserName())
                .RuleFor(x => x.Email, x => x.Internet.Email())
                .RuleFor(x => x.Password, x => x.Internet.Password());

            return register;
        }

        public static Login Login()
        {
            var login = new Faker<Login>()
                .RuleFor(x => x.Email, x => x.Internet.Email())
                .RuleFor(x => x.Password, x => x.Internet.Password());

            return login;
        }
    }
}