﻿using FluentValidation;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Validators.Categories
{
    public class UpdateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public UpdateCategoryValidator(Context context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => context.Categories.Any(y => y.Id == x))
                .WithMessage("The provided category Id does not exist.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("The name can not be null.")
                .MinimumLength(3)
                .WithMessage("The name of the category should have more than 2 characters.")
                .Must(x => !context.Categories.Any(y => y.Name == x))
                .WithMessage("A category with the name '{PropertyValue}' already exists.");
        }
    }
}
