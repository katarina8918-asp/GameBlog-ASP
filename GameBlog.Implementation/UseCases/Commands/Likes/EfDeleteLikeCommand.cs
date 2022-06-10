using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.Commands.Likes;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.Likes
{
    public class EfDeleteLikeCommand : EfUseCase, IDeleteLikeCommand
    {
        private readonly IApplicationUser _user;
        public EfDeleteLikeCommand(Context context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 12;

        public string Name => "Delete a Like";

        public string Description => "Delete a Like with EF";

        public void Execute(int request)
        {
            var like = Context.Likes.FirstOrDefault(x => x.PostId == request && x.UserId == _user.Id);

            if (like == null)
            {
                throw new Exception("Something went wrong.");
            }

            Context.Likes.Remove(like);
            Context.SaveChanges();
        }
    }
}
