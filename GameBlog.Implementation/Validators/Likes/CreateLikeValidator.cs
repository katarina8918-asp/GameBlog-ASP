using FluentValidation;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Validators.Likes
{
    public class CreateLikeValidator : AbstractValidator<LikeDto>
    {
        public CreateLikeValidator(Context context, IApplicationUser user)
        {
            RuleFor(x => x.PostId)
                .Must((dto, id) => !context.Likes.Any(y => y.PostId == dto.PostId && y.UserId == user.Id))
                .WithMessage("You have already liked this post.");
        }
    }
}
