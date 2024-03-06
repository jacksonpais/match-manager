using MatchManager.Domain.Common.Interface;
using MatchManager.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace MatchManager.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public DbSet<AppUserMaster> AppUserMaster { get; set; }
        public DbSet<UserActivation> UserActivation { get; set; }
        public DbSet<UserToken> UserToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUserMaster>().HasOne(e => e.Token).WithOne(ed => ed.UserMaster).HasForeignKey<UserToken>(ed => ed.UserId);
            modelBuilder.Entity<AppUserMaster>().HasMany(e => e.Activations).WithOne(ed => ed.UserMaster).HasForeignKey(ed => ed.UserId);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentDateTime = DateTime.Now;
            var entries = ChangeTracker.Entries().ToList();
            var updatedEntries = entries.Where(a => a.Entity is IAuditableBaseEntity).Where(a => a.State == EntityState.Added).ToList();
            updatedEntries.ForEach(e =>
            {
                ((IAuditableBaseEntity)e.Entity).CreatedDate = currentDateTime;
                ((IAuditableBaseEntity)e.Entity).UpdatedDate = currentDateTime;
            });
            return await base.SaveChangesAsync();
        }
    }
}
