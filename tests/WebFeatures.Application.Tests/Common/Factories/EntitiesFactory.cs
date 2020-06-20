using Bogus;
using Bogus.Extensions;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Permissions;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Tests.Common.Factories
{
    internal static class EntitiesFactory
    {
        public static class Users
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

        public static class Roles
        {
            public static Role Get()
            {
                var role = new Faker<Role>()
                    .StrictMode(true)
                    .RuleFor(x => x.Id, x => x.Random.Guid())
                    .RuleFor(x => x.Name, x => x.Lorem.Word())
                    .RuleFor(x => x.Description, x => x.Lorem.Sentence().OrNull(x));

                return role;
            }
        }

        public static class ProductCommens
        {
            public static ProductComment Get()
            {
                var comment = new Faker<ProductComment>()
                    .RuleFor(x => x.Id, x => x.Random.Guid());

                return comment;
            }
        }
    }
}