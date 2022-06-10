using AutoMapper;
using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.DTO;
using GameBlog.Application.UseCases.Queries.Categories;
using GameBlog.DataAccess;
using GameBlog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Queries.Ef.Categories
{
    public class EfGetOneCategoryQuery : EfUseCase, IGetOneCategoryQuery
    {
        //private readonly IMapper _mapper;
        public EfGetOneCategoryQuery(Context context) : base(context)
        {
            //_mapper = mapper;
        }

        public int Id => 3;

        public string Name => "Get one Category";

        public string Description => "Get one Category with EF";

        public OneCategoryDto Execute(int search)
        {
            var category = Context.Categories.FirstOrDefault(x => x.Id == search && x.IsActive);

            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), search);
            }

            return new OneCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Posts = category.Posts.Select(y => new PostDto
                {
                    Id = y.Id,
                    Title = y.Title,
                    Text = y.Text
                })
            };

        }
    }
}
