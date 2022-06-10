using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.DTO.Searches;
using GameBlog.Application.UseCases.Queries;
using GameBlog.Application.UseCases.Queries.Posts;
using GameBlog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Queries.Ef.Posts
{
    public class EfGetPostsQuery : EfUseCase, IGetPostsQuery
    {
        public EfGetPostsQuery(Context context) : base(context)
        {
        }

        public int Id => 4;

        public string Name => "Search Posts";

        public string Description => "Search Posts with EF";

        public PagedResponse<PostsSearchDto> Execute(BasePagedSearch search)
        {
            var query = Context.Posts.Include(x => x.User).AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Title.Contains(search.Keyword) || x.Text.Contains(search.Keyword));
            }
            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 6;
            }
            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }
            var skipItems = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<PostsSearchDto>();
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipItems).Take(search.PerPage.Value).Select(x => new PostsSearchDto
            {
                Id = x.Id,
                CreatedAt = DateTime.Parse(x.CreatedAt.ToString()).ToString(),
                CategoryId = x.CategoryId,
                Category = Context.Categories.Where(y => y.Id == x.CategoryId).Select(y => y.Name).FirstOrDefault().ToString(),
                Title = x.Title,
                UserId = x.UserId,
                User = x.User.FirstName + " " + x.User.LastName,
                TotalLikes = x.Likes.Where(y => y.PostId == x.Id).Count(),
                TotalComments = x.Comments.Where(y => y.PostId == x.Id).Count()
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
