using AutoMapper;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using WebFeatures.Application.Features.ProductComments.CreateProductComment;
using WebFeatures.Application.Features.ProductReviews.CreateProductReview;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Features.Products.UpdateProduct;
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
            MapperConfiguration sut = CreateConfiguration();

            // Act
            Action actual = () => sut.AssertConfigurationIsValid();

            // Assert
            actual.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(MappingData))]
        public void ShouldSupportMapping(Type source, Type destination)
        {
            // Arrange
            MapperConfiguration configuration = CreateConfiguration();

            IMapper sut = configuration.CreateMapper();

            object instance = Activator.CreateInstance(source);

            // Act
            Action actual = () => sut.Map(instance, source, destination);

            // Assert
            actual.Should().NotThrow();
        }
    }

    public class MappingData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new[] { typeof(CreateProductCommentCommand), typeof(ProductComment) };
            yield return new[] { typeof(CreateProductReviewCommand), typeof(ProductReview) };
            yield return new[] { typeof(CreateProductCommand), typeof(Product) };
            yield return new[] { typeof(UpdateProductCommand), typeof(Product) };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}