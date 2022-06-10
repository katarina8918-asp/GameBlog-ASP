using GameBlog.Application;
using GameBlog.Application.Logging;
using GameBlog.Application.UseCases;
using GameBlog.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation
{
    public class UseCaseHandler
    {
        private readonly IExceptionLogger _logger;
        private readonly IApplicationUser _user;
        private readonly IUseCaseLogger _useCaseLogger;
        public UseCaseHandler(IExceptionLogger logger, IApplicationUser user, IUseCaseLogger useCaseLogger)
        {
            _logger = logger;
            _user = user;
            _useCaseLogger = useCaseLogger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            try
            {
                HandleAuthAndLogging(command, data);

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                command.Execute(data);
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                _logger.Log(e);
                throw;
            }
        }

        public TResponse HandleQuery<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest data)
        {
            try
            {
                HandleAuthAndLogging(query, data);

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var response = query.Execute(data);
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
                return response;
            }
            catch (Exception e)
            {
                _logger.Log(e);
                throw;
            }
        }

        private void HandleAuthAndLogging<TRequest>(IUseCase useCase, TRequest data)
        {
            var log = new Application.UseCases.UseCaseLog
            {
                UseCaseName = useCase.Name,
                User = _user.Identity,
                UserId = _user.Id,
                ExecutionDateTime = DateTime.UtcNow,
                Data = JsonConvert.SerializeObject(data),
                IsAuthorized = _user.UseCaseIds.Contains(useCase.Id)
            };

            _useCaseLogger.Log(log);

            if (!log.IsAuthorized)
            {
                _useCaseLogger.Log(log);
                throw new ForbiddenUseCaseExecutionException(useCase.Name, _user.Identity);
            }
        }
    }
}
