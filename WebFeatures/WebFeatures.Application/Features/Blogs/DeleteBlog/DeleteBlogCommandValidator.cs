using FluentValidation;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces.Data;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.DeleteBlog
{
    public class DeleteBlogCommandValidator : AbstractValidator<DeleteBlogCommand>
    {
        public DeleteBlogCommandValidator(IRepository<Blog, int> blogRepo)
        {
            RuleFor(x => x.Id)
                .Must(blogRepo.Exists)
                .WithMessage(ValidationErrorMessages.MissingInDataBase(typeof(Blog)));
        }
    }
}
