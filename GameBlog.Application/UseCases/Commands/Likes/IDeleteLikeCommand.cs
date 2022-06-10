using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Application.UseCases.Commands.Likes
{
    public interface IDeleteLikeCommand : ICommand<int>
    {
    }
}
