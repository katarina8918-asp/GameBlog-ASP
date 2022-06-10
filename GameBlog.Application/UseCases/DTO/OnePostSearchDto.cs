using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Application.UseCases.DTO
{
    public class OnePostSearchDto
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
        public IEnumerable<CommentsPostSearchDto> Comments { get; set; } = new List<CommentsPostSearchDto>();
        public IEnumerable<ImagesPostSearchDto> Images { get; set; } = new List<ImagesPostSearchDto>();
    }

    public class CommentsPostSearchDto
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
    }

    public class ImagesPostSearchDto
    {
        public string Path { get; set; }
    }
}
