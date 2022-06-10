using GameBlog.Api.Core.DTO;
using GameBlog.Application.UseCases.Commands.Posts;
using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.DTO.Searches;
using GameBlog.Application.UseCases.Queries.Posts;
using GameBlog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private UseCaseHandler _handler;
        public PostController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        // GET: api/<PostController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] IGetPostsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetOnePostQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<PostController>
        [HttpPost]
        public IActionResult Post([FromForm] CreatePostWithImageDTO post, [FromServices] ICreatePostCommand command)
        {
            if(post.Image != null)
            {
                foreach(var file in post.Image)
                {
                    var guid = Guid.NewGuid();
                    var extension = Path.GetExtension(file.FileName);

                    var newName = guid + extension;

                    var path = Path.Combine("wwwroot", "images", newName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    post.ImageName.Add(newName);
                }
            }

            _handler.HandleCommand(command, post);
            return StatusCode(201);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] CreatePostWithImageDTO post, [FromServices] IUpdatePostCommand command)
        {
            if (post.Image != null)
            {
                foreach (var file in post.Image)
                {
                    var guid = Guid.NewGuid();
                    var extension = Path.GetExtension(file.FileName);

                    var newName = guid + extension;

                    var path = Path.Combine("wwwroot", "images", newName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    post.ImageName.Add(newName);
                }
            }
            post.Id = id;
            _handler.HandleCommand(command, post);
            return NoContent();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeletePostCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        [HttpDelete("user/{id}")]
        public IActionResult Delete(int id, [FromServices] IDeletePersonalPostCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
