using GameBlog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.DataAccess.Configurations
{
    public class UseCaseLogConfiguration : IEntityTypeConfiguration<UseCaseLog>
    {
        public void Configure(EntityTypeBuilder<UseCaseLog> builder)
        {
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.UseCaseName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Data).IsRequired();

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.IsAuthorized).IsRequired();

            builder.HasIndex(x => x.UseCaseName);
            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
