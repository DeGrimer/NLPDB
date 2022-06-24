using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NLPDB.Web.Data.Configuration
{
    public class CategoryModelConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(3000);

            builder.HasMany(x => x.TasksAlg).WithMany(x => x.Categories);
            builder.HasMany(x => x.Algorithms).WithOne(x => x.Category);
        }
    }
}
