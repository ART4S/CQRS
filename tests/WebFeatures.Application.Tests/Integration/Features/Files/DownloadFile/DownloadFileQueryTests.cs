using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Files.DownloadFile;
using WebFeatures.Application.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Files.DownloadFile
{
    public class DownloadFileQueryTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldReturnFile()
        {
            // Arrange
            var request = new DownloadFileQuery() { Id = new Guid("0d837b9d-36da-49df-9618-ebf740c86990") };

            // Act
            FileDownloadDto file = await Mediator.SendAsync(request);

            // Assert
            file.Should().NotBeNull();
            file.Content.Should().NotBeEmpty();
        }
    }
}