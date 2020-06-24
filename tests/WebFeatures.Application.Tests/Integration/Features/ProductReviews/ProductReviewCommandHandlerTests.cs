using Bogus;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;
using WebFeatures.Application.Features.ProductReviews.Requests.Commands;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Domian.Entities.Products;
using WebFeatures.Domian.Enums;
using Xunit;
using ValidationException = WebFeatures.Application.Exceptions.ValidationException;

namespace WebFeatures.Application.Tests.Integration.Features.ProductReviews
{
    public class ProductReviewCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task CreateProductReview_CreatesReview()
        {
            // Arrange
            var faker = new Faker();

            var request = new CreateProductReview()
            {
                ProductId = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e"),
                Title = faker.Lorem.Sentence(),
                Comment = faker.Lorem.Sentences(),
                Rating = faker.PickRandom<ProductRating>()
            };

            // Act
            Guid adminId = await AuthenticateAdminAsync();

            Guid reviewId = await Mediator.SendAsync(request);

            ProductReview review = await DbContext.ProductReviews.GetAsync(reviewId);

            // Assert
            review.Id.Should().Be(reviewId);
            review.Title.Should().Be(request.Title);
            review.Comment.Should().Be(request.Comment);
            review.Rating.Should().Be(request.Rating);
            review.ProductId.Should().Be(request.ProductId);
            review.AuthorId.Should().Be(adminId);
            review.CreateDate.Should().BeCloseTo(DateTime.UtcNow, 1000);
        }

        [Fact]
        public async Task CreateProductReview_WhenInvalidReview_Throws()
        {
            // Arrange
            var request = new CreateProductComment();

            // Act
            Func<Task<Guid>> actual = () => Mediator.SendAsync(request);

            // Assert
            (await actual.Should().ThrowAsync<ValidationException>()).And.Error.Should().NotBeNull();
        }
    }
}