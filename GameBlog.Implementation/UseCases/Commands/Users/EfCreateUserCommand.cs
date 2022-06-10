using FluentValidation;
using GameBlog.Application.UseCases.Commands.Users;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.Users
{
    public class EfCreateUserCommand : EfUseCase, ICreateUserCommand
    {
        private readonly CreateUserValidator _validator;
        public EfCreateUserCommand(Context context, CreateUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 21;

        public string Name => "Create new User";

        public string Description => "Create new User with EF";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            string myPassword = request.Password;
            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
            string myHash = BCrypt.Net.BCrypt.HashPassword(myPassword, mySalt);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                Password = myHash
            };

            Context.Users.Add(user);
            Context.SaveChanges();

            var useCases = Enumerable.Range(8, 2).ToList();

            useCases.ForEach(x => Context.UserUseCases.Add(new UserUseCase
            {
                UserId = user.Id,
                UserUseCaseId = x
            }));

            Context.SaveChanges();

        }
    }
}
