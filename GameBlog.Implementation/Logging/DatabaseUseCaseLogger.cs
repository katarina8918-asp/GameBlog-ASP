using GameBlog.Application.UseCases;
using GameBlog.DataAccess;
using GameBlog.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Logging
{
    public class DatabaseUseCaseLogger : IUseCaseLogger
    {
        private readonly Context _context;
        private readonly IApplicationUser _user;

        public DatabaseUseCaseLogger(Context context, IApplicationUser user)
        {
            _context = context;
            _user = user;
        }

        public void Log(Application.UseCases.UseCaseLog log)
        {
            _context.UseCaseLogs.Add(new Domain.UseCaseLog
            {
                CreatedAt = DateTime.UtcNow,
                UseCaseName = log.UseCaseName,
                Data = JsonConvert.SerializeObject(log.Data),
                UserId = _user.Id,
                Email = _user.Identity,
                IsAuthorized = log.IsAuthorized
            });

            _context.SaveChanges();
        }
    }
}
