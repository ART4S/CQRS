using FluentValidation;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator(IRepository<User, int> userRepo)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.AuthorId)
                .Must(userRepo.Exists)
                .WithMessage(ValidationErrorMessages.MissingInDataBase(typeof(User)));
        }
    }
}
