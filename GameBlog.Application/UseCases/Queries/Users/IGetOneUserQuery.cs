using GameBlog.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Application.UseCases.Queries.Users
{
    public interface IGetOneUserQuery : IQuery<int, OneUserDto>
    {
    }
}
