using GameBlog.Application.UseCases.Commands.Likes;
using GameBlog.Application.UseCases.DTO;
using GameBlog.Implementation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private UseCaseHandler _handler;
        public LikeController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        // GET: api/<LikeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LikeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LikeController>
        [HttpPost]
        public IActionResult Post([FromBody] LikeDto like, [FromServices] ICreateLikeCommand command)
        {
            _handler.HandleCommand(command, like);
            return StatusCode(201);
        }

        // PUT api/<LikeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LikeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteLikeCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
