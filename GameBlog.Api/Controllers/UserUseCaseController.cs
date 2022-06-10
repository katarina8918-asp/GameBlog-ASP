using GameBlog.Application.UseCases.Commands;
using GameBlog.Application.UseCases.DTO;
using GameBlog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserUseCaseController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UserUseCaseController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<UserUseCasesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserUseCasesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserUseCasesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserUseCasesController>
        [HttpPut]
        public IActionResult Put([FromBody] UpdateUserUseCasesDto dto, [FromServices] IUpdateUserUseCasesCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<UserUseCasesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
