using AutoMapper;
using FluentValidation;
using GameBlog.Application.Exceptions;
using GameBlog.Application.UseCases.Commands.Posts;
using GameBlog.Application.UseCases.DTO;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.Validators.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.UseCases.Commands.Posts
{
    public class EfUpdatePostCommand : EfUseCase, IUpdatePostCommand
    {
        private readonly CreatePostValidator _validator;
        private readonly IApplicationUser _user;
        private readonly IMapper _mapper;
        public EfUpdatePostCommand(Context context, CreatePostValidator validator, IApplicationUser user, IMapper mapper) : base(context)
        {
            _validator = validator;
            _user = user;
            _mapper = mapper;
        }

        public int Id => 14;

        public string Name => "Update a Post";

        public string Description => "Update a Post with EF";

        public void Execute(CreatePostDto request)
        {
            _validator.ValidateAndThrow(request);

            var post = Context.Posts.Find(request.Id);
            
            if (post == null)
            {
                throw new EntityNotFoundException(nameof(Post), request.Id);
            }
            request.UserId = post.UserId;
            
            _mapper.Map(request, post);

            if (request.ImageName.Count() > 0)
            {
                foreach (var image in request.ImageName)
                {
                    var img = new Image
                    {
                        Path = image,
                        Post = post
                    };
                    Context.Images.Add(img);
                }
            }
            post.ModifiedBy = _user.Identity;
            Context.SaveChanges();
        }
    }
}
