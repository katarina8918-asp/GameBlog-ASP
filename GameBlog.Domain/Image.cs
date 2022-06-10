using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Domain
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
