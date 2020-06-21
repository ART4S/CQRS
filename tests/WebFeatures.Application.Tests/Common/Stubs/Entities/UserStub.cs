using Bogus;
using Bogus.Extensions;
using System;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Tests.Common.Stubs.Entities
{
    internal class UserStub : Faker<User>
    {
        public UserStub()
        {
            StrictMode(true);

            RuleForType(typeof(Guid), x => x.Random.Guid());
            RuleForType(typeof(Guid?), x => ((Guid?)x.Random.Guid()).OrDefault(x));

            RuleFor(x => x.Name, x => x.Internet.UserName());
            RuleFor(x => x.PasswordHash, x => x.Random.Utf16String());
            RuleFor(x => x.Email, x => x.Internet.Email());

            Ignore(x => x.Picture);
        }
    }
}