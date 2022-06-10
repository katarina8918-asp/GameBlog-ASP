using GameBlog.Application.UseCases.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GameBlog.Api.Core.DTO
{
    public class CreatePostWithImageDTO : CreatePostDto
    {
        public List<IFormFile> Image { get; set; }
    }
}
