using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Application.UseCases.DTO
{
    public class PostsSearchDto
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
    }
}
