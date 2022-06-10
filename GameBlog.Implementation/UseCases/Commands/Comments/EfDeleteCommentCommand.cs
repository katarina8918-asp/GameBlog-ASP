using FluentValidation;
using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.Commands.Comments;
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
    public class EfDeleteCommentCommand : EfUseCase, IDeleteCommentCommand
    {
        private readonly DeleteCommentValidator _validator;
        private readonly IApplicationUser _user;
        public EfDeleteCommentCommand(Context context, DeleteCommentValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 20;

        public string Name => "Delete Comment";

        public string Description => "Delete Comment with EF";

        public void Execute(int request)
        {
            _validator.ValidateAndThrow(request);
            var comment = Context.Comments.Find(request);

            if (comment == null)
            {
                throw new EntityNotFoundException(nameof(Comment), request);
            }

            comment.IsActive = false;
            comment.IsDeleted = true;
            comment.DeletedAt = DateTime.Now;
            comment.DeletedBy = _user.Identity;
            Context.SaveChanges();
        }
    }
}
