using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Domain
{
    public class UserUseCase 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserUseCaseId { get; set; }
        public virtual User User { get; set; }
    }
}
