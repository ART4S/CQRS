using Bogus;
using Bogus.Extensions;
using System;
using WebFeatures.Domian.Entities.Permissions;

namespace WebFeatures.Application.Tests.Common.Stubs.Entities
{
    internal class RoleStub : Faker<Role>
    {
        public RoleStub()
        {
            StrictMode(true);

            RuleForType(typeof(Guid), x => x.Random.Guid());

            RuleFor(x => x.Name, x => x.Lorem.Word());
            RuleFor(x => x.Description, x => x.Lorem.Sentence().OrNull(x));
        }
    }
}