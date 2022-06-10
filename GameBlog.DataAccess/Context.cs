using GameBlog.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace GameBlog.DataAccess
{
    public class Context : DbContext
    {
        
        public Context() { }

        public Context(DbContextOptions options) : base(options)
        {

        }
        public Context(IApplicationUser user)
        {
            User = user;
        }

        public IApplicationUser User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<Like>().HasKey(x => new {x.PostId, x.UserId});
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Post>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MD1199Q;Initial Catalog=AspGameBlog2;Integrated Security=True").UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.CreatedAt = DateTime.UtcNow;
                            e.ModifiedAt = null;
                            e.DeletedAt = null;
                            e.IsDeleted = false;
                            e.IsActive = true;
                            break;
                        case EntityState.Modified:
                            e.ModifiedAt = DateTime.UtcNow;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }

        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
