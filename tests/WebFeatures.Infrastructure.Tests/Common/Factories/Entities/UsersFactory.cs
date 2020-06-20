using Bogus;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Infrastructure.Tests.Common.Factories.Entities
{
    public static class UsersFactory
    {
        public static User Get()
        {
            var user = new Faker<User>()
                .StrictMode(true)
                .RuleFor(x => x.Id, x => x.Random.Guid())
                .RuleFor(x => x.Name, x => x.Internet.UserName())
                .RuleFor(x => x.PasswordHash, x => x.Random.Utf16String())
                .RuleFor(x => x.Email, x => x.Internet.Email())
                .Ignore(x => x.PictureId)
                .Ignore(x => x.Picture);

            return user;
        }
    }
}