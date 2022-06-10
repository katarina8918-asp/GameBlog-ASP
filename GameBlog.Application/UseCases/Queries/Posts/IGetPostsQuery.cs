using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Application.UseCases.Queries.Posts
{
    public interface IGetPostsQuery : IQuery<BasePagedSearch, PagedResponse<PostsSearchDto>>
    {
    }
}
