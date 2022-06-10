using FluentValidation;
using GameBlog.Application.Email;
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

namespace GameBlog.Implementation.UseCases.Commands
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private RegisterUserValidator _validator;
        private IEmailSender _emailSender;
        public EfRegisterUserCommand(Context context, RegisterUserValidator validator, IEmailSender emailSender) : base(context)
        {
            _validator = validator;
            _emailSender = emailSender;
        }

        public int Id => 1;

        public string Name => "Create a new user.";

        public string Description => "Creating a new user with Entity Framework.";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            string myPassword = request.Password;
            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
            string myHash = BCrypt.Net.BCrypt.HashPassword(myPassword, mySalt);


            //var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

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

            _emailSender.SendEmail(new SendEmailDto
            {
                To = request.Email,
                Subject = "Confirm registration",
                Body = "Welcome. Please activate your account."
            });
        }
    }
}
