using FluentValidation;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Validators.Posts
{
    public class CreatePostValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostValidator(Context context)
        {
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("The title can not be empty.")
                .MinimumLength(15)
                .WithMessage("The title must be at least 15 characters long.")
                .MaximumLength(100)
                .WithMessage("The title can not be longer than 100 characters.")
                .Must(x => !context.Posts.Any(y => y.Title == x))
                .WithMessage("The provided title already exists.");

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("The text can not be empty.")
                .MinimumLength(20)
                .WithMessage("The text must be at least 20 characters long.");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category Id is required.")
                .Must(x => context.Categories.Any(y => y.Id == x && y.IsActive))
                .WithMessage("The chosen category Id does not exist.");

            
        }
    }
}
