using Bogus;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Tests.Common.Stubs.Entities
{
    internal class FileStub : Faker<File>
    {
        public FileStub()
        {
            RuleFor(x => x.Name, x => x.System.CommonFileName());
            RuleFor(x => x.ContentType, x => x.Lorem.Word());
            RuleFor(x => x.CheckSum, x => x.Random.Utf16String());
            RuleFor(x => x.Content, x => x.Random.Bytes(16));
        }
    }
}