using AutoMapper;
using FluentAssertions;
using System;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;
using WebFeatures.Application.Features.ProductReviews.Requests.Commands;
using WebFeatures.Application.Features.Products.Requests.Commands;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Domian.Entities.Products;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Infrastructure
{
    public class MappingsProfileTests
    {
        private MapperConfiguration CreateConfiguration()
        {
            return new MapperConfiguration(x => x.AddProfile<MappingsProfile>());
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            // Arrange
            MapperConfiguration configuration = CreateConfiguration();

            // Act
            Action actual = () => configuration.AssertConfigurationIsValid();

            // Assert
            actual.Should().NotThrow();
        }

        [Theory]
        [InlineData(typeof(CreateProductComment), typeof(ProductComment))]
        [InlineData(typeof(CreateProductReview), typeof(ProductReview))]
        [InlineData(typeof(CreateProduct), typeof(Product))]
        [InlineData(typeof(UpdateProduct), typeof(Product))]
        public void ShouldSupportMapping(Type source, Type destination)
        {
            // Arrange
            MapperConfiguration configuration = CreateConfiguration();

            IMapper mapper = configuration.CreateMapper();

            object instance = Activator.CreateInstance(source);

            // Act
            Action actual = () => mapper.Map(instance, source, destination);

            // Assert
            actual.Should().NotThrow();
        }
    }
}