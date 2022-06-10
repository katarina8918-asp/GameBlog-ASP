using AutoMapper;
using GameBlog.Application.UseCases.DTO;
using GameBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, CreatePostDto>();
            CreateMap<CreatePostDto, Post>();
        }
    }
}
