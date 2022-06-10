using FluentValidation;
using GameBlog.Application.UseCases.Commands.Comments;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.Validators.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.Comments
{
    public class EfCreateCommentCommand : EfUseCase, ICreateCommentCommand
    {
        private readonly CreateCommentValidator _validator;
        private readonly IApplicationUser _user;
        public EfCreateCommentCommand(Context context, CreateCommentValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 6;

        public string Name => "Create new Comment";

        public string Description => "Create new Comment with EF";

        public void Execute(CommentDto request)
        {
            _validator.ValidateAndThrow(request);
            var comment = new Comment
            {
                Text = request.Text,
                UserId = _user.Id,
                PostId = request.PostId
            };
            Context.Comments.Add(comment);
            Context.SaveChanges();
        }
    }
}
