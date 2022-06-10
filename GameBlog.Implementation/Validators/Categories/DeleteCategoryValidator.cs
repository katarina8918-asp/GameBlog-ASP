using FluentValidation;
using GameBlog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Validators.Categories
{
    public class DeleteCategoryValidator : AbstractValidator<int>
    {
        public DeleteCategoryValidator(Context context)
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}
