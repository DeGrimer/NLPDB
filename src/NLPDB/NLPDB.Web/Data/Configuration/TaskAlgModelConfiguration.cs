using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NLPDB.Web.Data.Configuration
{
    public class TaskAlgModelConfiguration : IEntityTypeConfiguration<TaskAlg>
    {
        public void Configure(EntityTypeBuilder<TaskAlg> builder)
        {
            builder.ToTable("TaskAlg");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id);
            builder.Property(t => t.Title).HasMaxLength(100).IsRequired();
            builder.Property(t => t.Description).HasMaxLength(2000);
        }
    }
}
