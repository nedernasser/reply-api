using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reply.Exam.WebAPI.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Infrastructure.EntityConfigurations
{
    public class BaseModelConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseModel
    {
        public string TableName { get; set; }

        public BaseModelConfiguration(string tableName)
        {
            this.TableName = tableName;
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(this.TableName);

            builder.HasKey(x => x.ID);

            builder
                .Property(x => x.ID)
                .UseIdentityColumn()
                .IsRequired();

            builder
                .Property(x => x.Deleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
