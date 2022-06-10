using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Application.UseCases.DTO
{
    public class OneCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PostDto> Posts { get; set; } = new List<PostDto>();
    }
}
