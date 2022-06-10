using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.Queries.Posts;
using GameBlog.DataAccess;
using GameBlog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Queries.Ef.Posts
{
    public class EfGetOnePostQuery : EfUseCase, IGetOnePostQuery
    {
        public EfGetOnePostQuery(Context context) : base(context)
        {
        }

        public int Id => 5;

        public string Name => "Get one Post";

        public string Description => "Get one Post with EF";

        public OnePostSearchDto Execute(int search)
        {
            var post = Context.Posts.Find(search);
            if (post == null)
            {
                throw new EntityNotFoundException(nameof(Post), search);
            }

            var query = Context.Posts
                .Include(x => x.Comments)
                .Include(x => x.Likes)
                .Include(x => x.User)
                .Where(x => x.Id == search)
                .First();

            var categoryName = Context.Categories.FirstOrDefault(x => x.Id == query.CategoryId);

            return new OnePostSearchDto
            {
                Id = query.Id,
                CreatedAt = DateTime.Parse(query.CreatedAt.ToString()).ToString(),
                CategoryId = query.CategoryId,
                Category = categoryName.Name,
                Title = query.Title,
                UserId = query.UserId,
                User = query.User.FirstName + " " + query.User.LastName,
                Text= query.Text,
                Images = query.Images.Select(x => new ImagesPostSearchDto
                {
                    Path = x.Path
                }),
                TotalLikes = query.Likes.Where(y => y.PostId == query.Id).Count(),
                TotalComments = query.Comments.Where(y => y.PostId == query.Id).Count(),
                Comments = query.Comments.Select(x => new CommentsPostSearchDto
                {
                    Id = x.Id,
                    CreatedAt = DateTime.Parse(x.CreatedAt.ToString()).ToString(),
                    UserId = x.UserId,
                    User = x.User.FirstName + " " + x.User.LastName,
                    Text = x.Text
                })
            };
        }
    }
}
