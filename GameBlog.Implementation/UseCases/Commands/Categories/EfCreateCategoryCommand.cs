using GameBlog.Application.UseCases.Commands.Categories;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.Validators.Categories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands
{
    public class EfCreateCategoryCommand : EfUseCase, ICreateCategoryCommand
    {
        private CreateCategoryValidator _validator;
        public EfCreateCategoryCommand(Context context, CreateCategoryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "Create Category";

        public string Description => "Creating Category with EF";

        public void Execute(CreateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);

            var category = new Category
            {
                Name = request.Name
            };
            Context.Categories.Add(category);
            Context.SaveChanges();
        }
    }
}
