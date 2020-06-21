using Bogus;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs.Entities
{
    internal class UserStub : Faker<User>
    {
        public UserStub()
        {
            StrictMode(true);

            RuleFor(x => x.Id, x => x.Random.Guid());
            RuleFor(x => x.Name, x => x.Internet.UserName());
            RuleFor(x => x.PasswordHash, x => x.Random.Utf16String());
            RuleFor(x => x.Email, x => x.Internet.Email());

            Ignore(x => x.PictureId);
            Ignore(x => x.Picture);
        }
    }
}