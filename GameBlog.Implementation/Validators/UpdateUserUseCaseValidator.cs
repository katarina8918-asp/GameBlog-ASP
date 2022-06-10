using FluentValidation;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Validators
{
    public class UpdateUserUseCaseValidator : AbstractValidator<UpdateUserUseCasesDto>
    {
        public UpdateUserUseCaseValidator(Context context)
        {
            RuleFor(x => x.UseCaseIds).NotEmpty()
                .WithMessage("UseCaseIds can not be empty.")
                .Must(x => x.Distinct().Count() == x.Count())
                .WithMessage("Duplicates are not allowed.");

            RuleFor(x => x.UserId).NotEmpty()
                .Must(x => context.Users.Any(user => user.Id == x && user.IsActive))
                .WithMessage("The provided user Id does not exist.");
        }
    }
}
