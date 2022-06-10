using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.Commands.Posts;
using GameBlog.DataAccess;
using GameBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.Posts
{
    public class EfDeletePostCommand : EfUseCase, IDeletePostCommand
    {
        private readonly IApplicationUser _user;
        public EfDeletePostCommand(Context context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 24;

        public string Name => "Delete a Post";

        public string Description => "Delete a Post with EF";

        public void Execute(int request)
        {
            var post = Context.Posts.Find(request);

            if (post == null)
            {
                throw new EntityNotFoundException(nameof(Post), request);
            }

            post.IsActive = false;
            post.IsDeleted = true;
            post.DeletedAt = DateTime.Now;
            post.DeletedBy = _user.Identity;

            var comments = post.Comments;

            if(comments != null)
            {
                foreach(var comment in comments)
                {
                    comment.IsActive = false;
                    comment.IsDeleted = true;
                    comment.DeletedAt = DateTime.Now;
                    comment.DeletedBy = _user.Identity;
                }
            }

            Context.SaveChanges();
        }
    }
}
