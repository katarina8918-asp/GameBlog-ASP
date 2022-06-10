using FluentValidation;
using GameBlog.Application.UseCases.Commands.Likes;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.Validators.Likes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.Likes
{
    public class EfCreateLikeCommand : EfUseCase, ICreateLikeCommand
    {
        private readonly CreateLikeValidator _validator;
        private readonly IApplicationUser _user;
        public EfCreateLikeCommand(Context context, CreateLikeValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 11;

        public string Name => "Create a Like";

        public string Description => "Create a Like with EF";

        public void Execute(LikeDto request)
        {
            _validator.ValidateAndThrow(request);

            var like = new Like
            {
                PostId = request.PostId,
                UserId = _user.Id
            };

            Context.Likes.Add(like);
            Context.SaveChanges();

        }
    }
}
