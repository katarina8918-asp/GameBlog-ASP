using FluentValidation;
using GameBlog.Application.UseCases.Commands.Posts;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.Validators.Posts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.Posts
{
    public class EfCreatePostCommand : EfUseCase, ICreatePostCommand
    {
        private readonly CreatePostValidator _validator;
        private readonly IApplicationUser _user;
        public EfCreatePostCommand(Context context, CreatePostValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 13;

        public string Name => "Create a Post";

        public string Description => "Create a Post with EF";

        public void Execute(CreatePostDto request)
        {
            _validator.ValidateAndThrow(request);

            var post = new Post
            {
                Title = request.Title,
                Text = request.Text,
                CategoryId = request.CategoryId,
                UserId = _user.Id
            };

            Context.Posts.Add(post);

            if(request.ImageName.Count() > 0)
            {
                foreach(var image in request.ImageName)
                {
                    var img = new Image
                    {
                        Path = image,
                        Post = post
                    };
                    Context.Images.Add(img);
                }
            }

            Context.SaveChanges();
        }
    }
}
