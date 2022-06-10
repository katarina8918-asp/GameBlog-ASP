using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.DTO.Searches;
using GameBlog.Application.UseCases.Queries;
using GameBlog.Application.UseCases.Queries.Users;
using GameBlog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Queries.Ef.Users
{
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        public EfGetUsersQuery(Context context) : base(context)
        {
        }

        public int Id => 9;

        public string Name => "Search Users";

        public string Description => "Search Users with EF";

        public PagedResponse<SearchUserDto> Execute(BasePagedSearch search)
        {
            var query = Context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Email.Contains(search.Keyword) || x.UserName.Contains(search.Keyword) ||
                    x.FirstName.Contains(search.Keyword) || x.LastName.Contains(search.Keyword));
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

            var response = new PagedResponse<SearchUserDto>();
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipItems).Take(search.PerPage.Value).Select(x => new SearchUserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                Email = x.Email
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
