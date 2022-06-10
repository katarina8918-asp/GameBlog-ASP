using FluentValidation;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Validators.Comments
{
    public class CreateCommentValidator : AbstractValidator<CommentDto>
    {
        public CreateCommentValidator(Context context)
        {
            RuleFor(x => x.Text).NotEmpty()
                .WithMessage("Comment can not be empty.");

            RuleFor(x => x.PostId)
                .NotEmpty()
                .Must(y => context.Posts.Any(p => p.Id == y))
                .WithMessage("The provided post Id does not exist.");
        }
    }
}
