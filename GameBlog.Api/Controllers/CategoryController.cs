using FluentValidation;
using GameBlog.Api.Extensions;
using GameBlog.Application;
using GameBlog.Application.Exceptions;
using GameBlog.Application.Logging;
using GameBlog.Application.UseCases.Commands.Categories;
using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.DTO.Searches;
using GameBlog.Application.UseCases.Queries;
using GameBlog.Application.UseCases.Queries.Categories;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation;
using GameBlog.Implementation.Validators.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static GameBlog.Api.Extensions.StringExtensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private UseCaseHandler _handler;
        public CategoryController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET api/<CategoryController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] IGetCategoriesQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetOneCategoryQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto category, [FromServices] ICreateCategoryCommand command)
        {
            _handler.HandleCommand(command, category);
            return StatusCode(201);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryDto category, [FromServices] IUpdateCategoryCommand command)
        {
            category.Id = id;
            _handler.HandleCommand(command, category);
            return StatusCode(204);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
