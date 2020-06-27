using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Files.UploadFile;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Application.Tests.Common.Stubs.Files;
using Xunit;
using File = WebFeatures.Domian.Entities.File;

namespace WebFeatures.Application.Tests.Integration.Features.Files.UploadFile
{
    public class UploadFileCommandTests : IntegrationTestBase
    {
        [Fact]
        public async Task HandleAsync_CreatesFile()
        {
            // Arrange
            var fileStub = new CustomFile();

            var request = new UploadFileCommand() { File = fileStub };

            // Act
            Guid fileId = await Mediator.SendAsync(request);

            File file = await DbContext.Files.GetAsync(fileId);

            // Assert
            file.Should().NotBeNull();
            file.Id.Should().Be(fileId);
            file.Name.Should().Be(fileStub.Name);
            file.ContentType.Should().Be(fileStub.ContentType);
            file.CheckSum.Should().NotBeNullOrWhiteSpace();
            file.Content.Should().Equal(fileStub.Content);
        }
    }
}