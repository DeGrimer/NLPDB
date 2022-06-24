using Calabonga.EntityFrameworkCore.Entities.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NLPDB.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            DbSaveChanges();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            DbSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            DbSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DbSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void DbSaveChanges()
        {
            const string defaultUser = "System";
            var defaultDate = DateTime.Now;

            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

            foreach(var entry in addedEntities)
            {
                if(entry.Entity is not IAuditable)
                {
                    return;
                }

                var createdAt = entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue;
                var updatedAt = entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue;
                var createdBy = entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue;
                var updatedBy = entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue;

                if (string.IsNullOrEmpty(createdBy?.ToString()))
                {
                    entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue = defaultUser;
                }
                if (string.IsNullOrEmpty(updatedBy?.ToString()))
                {
                    entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue = defaultUser;
                }
                if ((DateTime)createdAt == default(DateTime))
                {
                    entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue = defaultDate;
                }
                if (updatedAt != null && (DateTime)updatedAt == default(DateTime))
                {
                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
                }
                else
                {
                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
                }
            }

            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
        }
    }
}