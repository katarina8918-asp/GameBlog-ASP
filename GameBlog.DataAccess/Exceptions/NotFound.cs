using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.DataAccess.Exceptions
{
    public class NotFound : Exception
    {
        public NotFound(string? message)
        {

        }
    }
}
