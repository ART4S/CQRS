using Bogus;
using Bogus.Extensions;
using WebFeatures.Domian.Entities.Permissions;

namespace WebFeatures.Application.Tests.Common.Factories.Entities
{
    internal static class RoleFactory
    {
        public static Role Get()
        {
            var role = new Faker<Role>()
                .StrictMode(true)
                .RuleFor(x => x.Id, x => x.Random.Guid())
                .RuleFor(x => x.Name, x => x.Random.Utf16String())
                .RuleFor(x => x.Description, x => x.Random.Utf16String().OrNull(x));

            return role;
        }
    }
}
