using Bogus;
using System.IO;
using System.Net.Mime;
using WebFeatures.Application.Interfaces.Files;

namespace WebFeatures.Application.Tests.Common.Stubs.Files
{
    internal class CustomFile : IFile
    {
        private static readonly string[] ContentTypes =
        {
            MediaTypeNames.Image.Jpeg,
            MediaTypeNames.Application.Json
        };

        public CustomFile(params string[] extensions)
        {
            var faker = new Faker();

            Name = faker.System.FileName(faker.PickRandom(extensions));
            ContentType = faker.PickRandom(ContentTypes);
        }

        public string Name { get; }
        public string ContentType { get; }

        public Stream OpenReadStream()
        {
            return new MemoryStream();
        }
    }
}