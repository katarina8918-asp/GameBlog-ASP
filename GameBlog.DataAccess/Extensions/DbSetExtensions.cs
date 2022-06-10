using GameBlog.DataAccess.Exceptions;
using GameBlog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.DataAccess.Extensions
{
    public static class DbSetExtensions
    {
        public static void Deactivate(this DbContext context, Entity entity)
        {
            entity.IsActive = false;
            context.Entry(entity).State = EntityState.Modified;
        }

        public static void Deactivate<T>(this DbContext context, int id) where T : Entity
        {
            var item = context.Set<T>().Find(id); // obj/null
            if (item == null)
            {
                throw new NotFound("Could not find an item with the provided Id.");
            }
            item.IsActive = false;
        }
        public static void Deactivate<T>(this DbContext context, IEnumerable<int> ids) where T : Entity
        {
            var validIds = context.Set<T>().Where(x => ids.Contains(x.Id));

            foreach(var id in validIds)
            {
                id.IsActive = false;
            }
        }

    }
}
