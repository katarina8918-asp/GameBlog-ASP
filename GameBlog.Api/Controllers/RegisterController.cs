using FluentValidation;
using GameBlog.Api.Extensions;
using GameBlog.Application.UseCases.Commands;
using GameBlog.Application.UseCases.DTO;
using GameBlog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private UseCaseHandler _handler;

        public RegisterController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // POST api/<RegisterController>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] UserDto user, [FromServices] IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, user);
            return StatusCode(201);
        }

    }
}
