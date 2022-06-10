using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.DTO.Searches;
using GameBlog.Application.UseCases.Queries;
using GameBlog.Application.UseCases.Queries.Categories;
using GameBlog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Queries.Ef.Categories
{
    public class EfGetCategoriesQuery : EfUseCase, IGetCategoriesQuery
    {
        public EfGetCategoriesQuery(Context context) : base(context)
        {
        }

        public int Id => 2;

        public string Name => "Search Categories";

        public string Description => "Search Categories";

        public PagedResponse<CategoryDto> Execute(BasePagedSearch search)
        {
            var query = Context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(c => c.Name.Contains(search.Keyword));
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

            var response = new PagedResponse<CategoryDto>();
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipItems).Take(search.PerPage.Value).Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
