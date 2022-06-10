using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.Queries.Users;
using GameBlog.DataAccess;
using GameBlog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Queries.Ef.Users
{
    public class EfGetOneUserQuery : EfUseCase, IGetOneUserQuery
    {
        public EfGetOneUserQuery(Context context) : base(context)
        {
        }

        public int Id => 10;

        public string Name => "Get one User";

        public string Description => "Get one User with EF";

        public OneUserDto Execute(int search)
        {
            var user = Context.Users.Include(x => x.Posts).FirstOrDefault(x => x.Id == search && x.IsActive);
            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), search);
            }
            var query = Context.Categories.Include(x => x.Posts).Where(x => x.Id == search);
            return new OneUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Posts = user.Posts.Select(y => new PostDto
                {
                    Id = y.Id,
                    Title = y.Title,
                    Text = y.Text,
                    TotalLikes = y.Likes.Where(x => x.PostId == y.Id).Count()
                })
            };
        }
    }
}
