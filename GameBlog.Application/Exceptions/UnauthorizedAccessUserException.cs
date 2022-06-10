using GameBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Application.Exceptions
{
    public class UnAuthorizedAccessUserException : Exception
    {
        public UnAuthorizedAccessUserException(IApplicationUser user, string UseCaseName)
            : base($"User with identity: {user.Identity} with id: {user.Id} has tried to execute Use Case {UseCaseName}")
        {

        }
    }
}
