using FluentValidation;
using GameBlog.Application.UseCases.Commands;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.UserUseCases
{
    public class EfUpdateUserUseCasesCommand : EfUseCase, IUpdateUserUseCasesCommand
    {
        private UpdateUserUseCaseValidator _validator;
        public EfUpdateUserUseCasesCommand(Context context, UpdateUserUseCaseValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 19;

        public string Name => "Update User Use Cases";

        public string Description => "Update User Use Cases with EF";

        public void Execute(UpdateUserUseCasesDto request)
        {
            _validator.ValidateAndThrow(request);
            var existingUseCases = Context.UserUseCases.Where(x => x.UserId == request.UserId);
            Context.RemoveRange(existingUseCases);
            var newUseCases = request.UseCaseIds.Select(x => new UserUseCase
            {
                UserId = request.UserId,
                UserUseCaseId = x
            });
            Context.UserUseCases.AddRange(newUseCases);
            Context.SaveChanges();
        }
    }
}
