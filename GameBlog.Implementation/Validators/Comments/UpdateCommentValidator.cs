using FluentValidation;
using GameBlog.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Validators.Comments
{
    public class UpdateCommentValidator : AbstractValidator<CommentDto>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
