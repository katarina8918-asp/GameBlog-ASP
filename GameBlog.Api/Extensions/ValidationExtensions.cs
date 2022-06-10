using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace GameBlog.Api.Extensions
{
    public static class ValidationExtensions
    {
        public static UnprocessableEntityObjectResult ToUnprocessableEntity(this IEnumerable<ValidationFailure> errors)
        {
            var error = errors.Select(x => new
            {
                x.ErrorMessage,
                x.PropertyName
            });
            return new UnprocessableEntityObjectResult(error);
        }
    }
}
