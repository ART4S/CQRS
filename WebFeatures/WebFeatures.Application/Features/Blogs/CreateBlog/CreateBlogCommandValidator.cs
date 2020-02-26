﻿using FluentValidation;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces.Data;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator(IAppContext context)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.AuthorId)
                .Must(context.Exists<User, int>)
                .WithMessage(ValidationErrorMessages.MissingInDataBase(typeof(User)));
        }
    }
}
