using Bogus.DataSets;
using FluentAssertions;
using FluentValidation.Results;
using System.Linq;
using WebFeatures.Application.Infrastructure.Results;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Infrastructure
{
    public class ValidationErrorTests
    {
        [Fact]
        public void WhenDifferentPropertyNames_ShouldSetErrorForEachUniqueName()
        {
            // Arrange
            string[] properties =
            {
                "prop_1",
                "prop_2"
            };

            ValidationFailure[] failures = properties.Select(x => new ValidationFailure(x, new Lorem().Sentence())).ToArray();

            // Act
            var validation = new ValidationError(failures);

            // Assert
            validation.Errors.Should().HaveCount(2);
            validation.Errors[failures[0].PropertyName].Should().Equal(failures[0].ErrorMessage);
            validation.Errors[failures[1].PropertyName].Should().Equal(failures[1].ErrorMessage);
        }

        [Fact]
        public void WhenOnePropertyName_ShouldSetOneError()
        {
            // Arrange
            var lorem = new Lorem();

            const string propertyName = "prop_1";

            string[] errors =
            {
                lorem.Sentence(),
                lorem.Sentence()
            };

            ValidationFailure[] failures = errors.Select(x => new ValidationFailure(propertyName, x)).ToArray();

            // Act
            var validation = new ValidationError(failures);

            // Assert
            validation.Errors.Should().HaveCount(1);
            validation.Errors[propertyName].Should().Equal(errors);
        }
    }
}