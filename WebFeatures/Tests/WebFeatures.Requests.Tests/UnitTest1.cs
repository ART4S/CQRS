using Moq;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Requests.Internal;
using WebFeatures.Requests.Services;
using Xunit;

namespace WebFeatures.Requests.Tests
{
    public class RequestMediatorTests
    {
        [Fact]
        public async Task SendAsync_ShouldReturnResult()
        {
            //// Arrange
            //string expectedResult = "test";

            //var requestMock = new Mock<IRequest<string>>();

            //var requestHandlerMock = new Mock<IRequestHandler<IRequest<string>, string>>();
            //requestHandlerMock.Setup(x => x.HandleAsync(
            //        It.IsAny<IRequest<string>>(),
            //        It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(() => expectedResult);

            //var serviceFactoryMock = new Mock<IServiceFactory>();

            //serviceFactoryMock.Setup(x => x.GetService<IRequestHandler<IRequest<string>, string>>())
            //    .Returns(() => requestHandlerMock.Object);

            //serviceFactoryMock.Setup(x => x.GetServices<IRequestHandler<IRequest<string>, string>>())
            //    .Returns(() => new IRequestHandler<IRequest<string>, string>[0]);

            //var mediator = new RequestMediator(serviceFactoryMock.Object);

            //// Act
            //string result = await mediator.SendAsync(requestMock.Object);

            //// Assert
            //result.ShouldBe(expectedResult);
        }
    }
}
