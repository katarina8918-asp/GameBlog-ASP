using GameBlog.Domain;
using System.Collections.Generic;

namespace GameBlog.Api.Core
{
    public class JwtUser : IApplicationUser
    {
        public string Identity { get; set; }

        public int Id { get; set; }

        public IEnumerable<int> UseCaseIds { get; set; } = new List<int>();

        public string Email { get; set; }
    }

    public class AnonymousUser : IApplicationUser
    {
        public string Identity => "Anonymous";

        public int Id => 0;

        public IEnumerable<int> UseCaseIds => new List<int> { 1,2,3,4,5 };

        public string Email => "anonymous@gameblog.com";
    }
}
