using FluentValidation;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.DeleteBlog
{
    public class DeleteBlogCommandValidator : AbstractValidator<DeleteBlogCommand>
    {
        public DeleteBlogCommandValidator(IAppContext context)
        {
            RuleFor(x => x.Id)
                .Must(context.Exists<Blog>)
                .WithMessage(ValidationErrorMessages.NotExistsInDb(typeof(Blog)));
        }
    }
}
