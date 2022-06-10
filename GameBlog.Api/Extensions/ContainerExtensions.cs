using GameBlog.Api.Core;
using GameBlog.Application.UseCases.Commands;
using GameBlog.Application.UseCases.Commands.Categories;
using GameBlog.Application.UseCases.Commands.Comments;
using GameBlog.Application.UseCases.Commands.Likes;
using GameBlog.Application.UseCases.Commands.Posts;
using GameBlog.Application.UseCases.Commands.Users;
using GameBlog.Application.UseCases.Queries;
using GameBlog.Application.UseCases.Queries.Categories;
using GameBlog.Application.UseCases.Queries.Posts;
using GameBlog.Application.UseCases.Queries.Users;
using GameBlog.DataAccess;
using GameBlog.Domain;
using GameBlog.Implementation.UseCases.Commands;
using GameBlog.Implementation.UseCases.Commands.Categories;
using GameBlog.Implementation.UseCases.Commands.Comments;
using GameBlog.Implementation.UseCases.Commands.Likes;
using GameBlog.Implementation.UseCases.Commands.Posts;
using GameBlog.Implementation.UseCases.Commands.Users;
using GameBlog.Implementation.UseCases.Commands.UserUseCases;
using GameBlog.Implementation.UseCases.Queries.Ef;
using GameBlog.Implementation.UseCases.Queries.Ef.Categories;
using GameBlog.Implementation.UseCases.Queries.Ef.Posts;
using GameBlog.Implementation.UseCases.Queries.Ef.Users;
using GameBlog.Implementation.Validators;
using GameBlog.Implementation.Validators.Categories;
using GameBlog.Implementation.Validators.Comments;
using GameBlog.Implementation.Validators.Likes;
using GameBlog.Implementation.Validators.Posts;
using GameBlog.Implementation.Validators.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameBlog.Api.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<Context>();
                var settings = x.GetService<AppSettings>();

                return new JwtManager(context, settings.JwtSettings);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddUseCases(this IServiceCollection services)
        {

            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            //usecaselogs
            services.AddTransient<IGetUseCaseLogsQuery, EfGetUseCaseLogsQuery>();
            //userusecases
            services.AddTransient<IUpdateUserUseCasesCommand, EfUpdateUserUseCasesCommand>();
            //categories
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<IGetOneCategoryQuery, EfGetOneCategoryQuery>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
            //comments
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<IUpdateCommentCommand, EfUpdateCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();
            services.AddTransient<IDeletePersonalCommentCommand, EfDeletePersonalCommentCommand>();
            //users
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetOneUserQuery, EfGetOneUserQuery>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            //likes
            services.AddTransient<ICreateLikeCommand, EfCreateLikeCommand>();
            services.AddTransient<IDeleteLikeCommand, EfDeleteLikeCommand>();
            //posts
            services.AddTransient<IGetPostsQuery, EfGetPostsQuery>();
            services.AddTransient<IGetOnePostQuery, EfGetOnePostQuery>();
            services.AddTransient<ICreatePostCommand, EfCreatePostCommand>();
            services.AddTransient<IUpdatePostCommand, EfUpdatePostCommand>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();
            services.AddTransient<IDeletePersonalPostCommand, EfDeletePersonalPostCommand>();

            // Validators
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<DeleteCategoryValidator>();
            services.AddTransient<UpdateCategoryValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<UpdateUserUseCaseValidator>();
            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<UpdateCommentValidator>();
            services.AddTransient<DeleteCommentValidator>();
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<DeleteUserValidator>();
            services.AddTransient<CreateLikeValidator>();
            services.AddTransient<CreatePostValidator>();
        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
              {
                  var accessor = x.GetService<IHttpContextAccessor>();
                  var header = accessor.HttpContext.Request.Headers["Authorization"];

                  var user = accessor.HttpContext.User;

                  if(user == null || user.FindFirst("UserId") == null)
                  {
                      return new AnonymousUser();
                  }
                  var actor = new JwtUser
                  {
                      Identity = user.FindFirst("Email").Value,
                      Id = Int32.Parse(user.FindFirst("UserId").Value),
                      Email = user.FindFirst("Email").Value,
                      UseCaseIds = JsonConvert.DeserializeObject<List<int>>(user.FindFirst("UseCases").Value)
                  };
                  return actor;
  
              });
        }
        public static void AddMapper(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(EfGetOneCategoryQuery).Assembly);
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(EfUpdateUserCommand).Assembly);
            services.AddAutoMapper(typeof(EfUpdatePostCommand).Assembly);
        }
        public static void AddContext(this IServiceCollection services)
        {
            services.AddTransient(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                var connectionString = x.GetService<AppSettings>().ConnectionString;

                optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();

                var options = optionsBuilder.Options;
                return new Context(options);
            });
        }
    }
}
