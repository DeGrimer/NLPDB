using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NLPDB.Web.Data.Configuration
{
    public class AlgorithmModelConfiguration : IEntityTypeConfiguration<Algorithm>
    {
        public void Configure(EntityTypeBuilder<Algorithm> builder)
        {
            builder.ToTable("Algorithm");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Content).HasMaxLength(3000).IsRequired();
            builder.Property(x => x.Link);
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.CreatedAt);

            builder.HasMany(x => x.TasksAlg).WithMany(x => x.Algorithms);
        }
    }
}
