using System.Threading.Tasks;
using Xunit;

namespace WebFeatures.Requests.Tests
{
    public class RequestMediatorTests
    {
        [Fact]
        public async Task SendAsync_ShouldReturnHandledResult()
        {
            //// Arrange
            //var callChecker = new CallChecker();

            //var serviceFactoryMock = new Mock<IServiceFactory>();

            //serviceFactoryMock.Setup(x => x.GetService(
            //        It.Is<Type>(x => x == typeof(IRequestHandler<TestRequest, TestRequestResult>))))
            //    .Returns(new TestRequestHandler(callChecker));

            //serviceFactoryMock.Setup(x => x.GetService(
            //        It.Is<Type>(x => x == typeof(IEnumerable<IRequestMiddleware<TestRequest, TestRequestResult>>))))
            //    .Returns(new IRequestMiddleware<TestRequest, TestRequestResult>[0]);

            //var mediator = new RequestMediator(serviceFactoryMock.Object);

            //// Act
            //TestRequestResult result = await mediator.SendAsync(new TestRequest());

            //// Assert
            //result.ShouldNotBeNull();
            //callChecker.Messages.ShouldHaveSingleItem();
            //callChecker.Messages.First().ShouldBe("Handler finished");
        }

        [Fact]
        public async Task SendAsync_ShouldCallMiddlewaresInRegistrationOrder()
        {
            //// Arrange
            //var callChecker = new CallChecker();

            //var serviceFactoryMock = new Mock<IServiceFactory>();

            //serviceFactoryMock.Setup(x => x.GetService(It.Is<Type>(x => x == typeof(IRequestHandler<TestRequest, TestRequestResult>))))
            //    .Returns(new TestRequestHandler(callChecker));

            //serviceFactoryMock.Setup(x => x.GetService(It.Is<Type>(x => x == typeof(IEnumerable<IRequestMiddleware<TestRequest, TestRequestResult>>))))
            //    .Returns(new IRequestMiddleware<TestRequest, TestRequestResult>[]
            //    {
            //        new OutherTestMiddleware(callChecker),
            //        new InnerTestMiddleware(callChecker)
            //    });

            //var mediator = new RequestMediator(serviceFactoryMock.Object);

            //// Act
            //await mediator.SendAsync(new TestRequest());

            //// Assert
            //callChecker.Messages.ShouldBe(new[]
            //{
            //    "Outher started",
            //    "Inner started",
            //    "Handler finished",
            //    "Inner finished",
            //    "Outher finished"
            //});
        }
    }
}
