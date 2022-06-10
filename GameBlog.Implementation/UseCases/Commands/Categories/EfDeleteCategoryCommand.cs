using FluentValidation;
using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.Commands.Categories;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.Validators.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.Categories
{
    public class EfDeleteCategoryCommand : EfUseCase, IDeleteCategoryCommand
    {
        private readonly DeleteCategoryValidator _validator;
        public EfDeleteCategoryCommand(Context context, DeleteCategoryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 17;

        public string Name => "Delete Category.";

        public string Description => "Deleting Category with EF";

        public void Execute(int request)
        {
            _validator.ValidateAndThrow(request);

            var category = Context.Categories.Include( x => x.Posts).FirstOrDefault(x => x.Id == request && x.IsActive);
            
            if(category == null)
            {
                throw new EntityNotFoundException(nameof(Category), request);
            }
            
            if (category.Posts.Any())
            {
                throw new UseCaseConflictException("The provided Category is linked to Posts.");
            }

            Context.Remove(category);

            Context.SaveChanges();
        }
    }
}
