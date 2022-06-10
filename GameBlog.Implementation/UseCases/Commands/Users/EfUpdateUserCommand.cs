using AutoMapper;
using FluentValidation;
using GameBlog.Application.Exceptions;
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
    public class EfUpdateUserCommand : EfUseCase, IUpdateUserCommand
    {
        private readonly UpdateUserValidator _validator;
        private readonly IApplicationUser _user;
        private readonly IMapper _mapper;
        public EfUpdateUserCommand(Context context, UpdateUserValidator validator, IApplicationUser user, IMapper mapper) : base(context)
        {
            _validator = validator;
            _user = user;
            _mapper = mapper;
        }

        public int Id => 22;

        public string Name => "Update User";

        public string Description => "Update User with EF";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = Context.Users.Find(request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), request.Id);
            }

            _mapper.Map(request, user);

            string myPassword = request.Password;
            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
            string myHash = BCrypt.Net.BCrypt.HashPassword(myPassword, mySalt);

            user.Password = myHash;
            user.ModifiedBy = _user.Identity;
            Context.SaveChanges();

        }
    }
}
