using FluentValidation;
using GameBlog.Application;
using GameBlog.Application.Exceptions;
using GameBlog.Application.Logging;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace GameBlog.Api.Core
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionLogger _logger;
        public GlobalExceptionHandler(RequestDelegate next, IExceptionLogger logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception e)
            {
                _logger.Log(e);

                httpContext.Response.ContentType = "application/json"; //header
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;

                if (e is ForbiddenUseCaseExecutionException)
                {
                    statusCode = StatusCodes.Status403Forbidden;
                }
                if (e is EntityNotFoundException)
                {
                    statusCode = StatusCodes.Status404NotFound;
                }
                if(e is System.Net.Mail.SmtpException)
                {
                    statusCode = StatusCodes.Status201Created;
                    // zbog problema sa mailom
                }
                if (e is ValidationException ex)
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    response = new
                    {
                        errors = ex.Errors.Select(x => new
                        {
                            property = x.PropertyName,
                            error = x.ErrorMessage
                        })
                    };
                }
                if (e is UseCaseConflictException conflictEx)
                {
                    statusCode = StatusCodes.Status409Conflict;
                    response = new { message = conflictEx.Message };
                }

                httpContext.Response.StatusCode = statusCode;
                if(response != null)
                {
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
                
            }
        }
    }
}
