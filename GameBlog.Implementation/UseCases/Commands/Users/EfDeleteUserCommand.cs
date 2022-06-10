using FluentValidation;
using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.Commands.Users;
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
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        private readonly DeleteUserValidator _validator;
        private readonly IApplicationUser _user;
        public EfDeleteUserCommand(Context context, DeleteUserValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 23;

        public string Name => "Delete User";

        public string Description => "Delete User with EF";

        public void Execute(int request)
        {
            _validator.ValidateAndThrow(request);

            var user = Context.Users.Find(request);
            var userPosts = Context.Posts.Where(x => x.UserId == request);

            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), request);
            }

            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;
            user.DeletedBy = _user.Identity;
            Context.SaveChanges();

            if(userPosts != null)
            {
                foreach (var post in userPosts)
                {
                    post.IsActive = false;
                    post.IsDeleted = true;
                    user.DeletedAt = DateTime.Now;
                    user.DeletedBy = _user.Identity;
                }
            }
            Context.SaveChanges();
        }
    }
}
