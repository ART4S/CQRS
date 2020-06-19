using Bogus;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Infrastructure.Tests.Common.Factories
{
    internal static class UserFactory
    {
        public static User Get()
        {
            var user = new Faker<User>()
                .RuleFor(x => x.Id, x => x.Random.Guid())
                .RuleFor(x => x.Name, x => x.Random.Utf16String())
                .RuleFor(x => x.PasswordHash, x => x.Random.Utf16String())
                .RuleFor(x => x.Email, x => x.Internet.Email());

            return user;
        }
    }
}