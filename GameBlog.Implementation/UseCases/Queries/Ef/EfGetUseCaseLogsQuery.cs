using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.DTO.Searches;
using GameBlog.Application.UseCases.Queries;
using GameBlog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Queries.Ef
{
    public class EfGetUseCaseLogsQuery : EfUseCase, IGetUseCaseLogsQuery
    {

        public EfGetUseCaseLogsQuery(Context context) : base(context)
        {
        }

        public int Id => 25;

        public string Name => "Search UseCase Logs";

        public string Description => "Search UseCase Logs with EF";

        public PagedResponse<UseCaseLogsDto> Execute(BasePagedSearch search)
        {
            var logs = Context.UseCaseLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                logs = logs.Where(x => x.UseCaseName.Contains(search.Keyword) || x.UseCaseName.Contains(search.Keyword));
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

            var response = new PagedResponse<UseCaseLogsDto>();
            response.TotalCount = logs.Count();

            response.Items = logs.Skip(skipItems).Take(search.PerPage.Value).Select(x => new UseCaseLogsDto
            {
                Id = x.Id,
                CreatedAt = DateTime.Parse(x.CreatedAt.ToString()),
                UseCaseName = x.UseCaseName,
                Data = x.Data,
                UserId = x.UserId,
                Email = x.Email,
                IsAuthorized = x.IsAuthorized
            });

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
