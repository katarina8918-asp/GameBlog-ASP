using FluentValidation;
using GameBlog.Application.Exceptions;
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
    public class EfUpdateCommentCommand : EfUseCase, IUpdateCommentCommand
    {
        private readonly UpdateCommentValidator _validator;
        private readonly IApplicationUser _user;
        public EfUpdateCommentCommand(Context context, UpdateCommentValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 7;

        public string Name => "Update Comment";

        public string Description => "Update Comment with EF";

        public void Execute(CommentDto request)
        {
            _validator.ValidateAndThrow(request);

            var comment = Context.Comments.Find(request.Id);

            if (comment == null)
            {
                throw new EntityNotFoundException(nameof(Comment), request.Id);
            }

            if (_user.Id != comment.UserId)
            {
                throw new UnAuthorizedAccessUserException(_user, Name);
            }

            comment.Text = request.Text;
            comment.ModifiedBy = _user.Identity;
            Context.SaveChanges();
        }
    }
}
