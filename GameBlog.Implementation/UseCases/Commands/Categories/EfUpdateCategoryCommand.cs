using AutoMapper;
using FluentValidation;
using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.Commands.Categories;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.Validators.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.Categories
{
    public class EfUpdateCategoryCommand : EfUseCase, IUpdateCategoryCommand
    {
        private readonly UpdateCategoryValidator _validator;
        private readonly IApplicationUser _user;
        public EfUpdateCategoryCommand(Context context, UpdateCategoryValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 18;

        public string Name => "Update Category";

        public string Description => "Updating Category with EF";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            var category = Context.Categories.Find(request.Id);
            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), request.Id);
            }
            category.Name = request.Name;
            category.ModifiedBy = _user.Identity;
            Context.SaveChanges();
        }
    }
}
