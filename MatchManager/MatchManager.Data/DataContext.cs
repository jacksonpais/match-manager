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

            modelBuilder.Entity<AppUserMaster>().Property(p => p.CreatedDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<AppUserMaster>().Property(p => p.UpdatedDate).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<UserToken>().Property(p => p.CreatedDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserToken>().Property(p => p.UpdatedDate).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<UserActivation>().Property(p => p.CreatedDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserActivation>().Property(p => p.UpdatedDate).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AppUserMaster>().HasOne(e => e.UserToken).WithOne(ed => ed.UserMaster).HasForeignKey<UserToken>(ed => ed.UserId);
            modelBuilder.Entity<AppUserMaster>().HasMany(e => e.UserActivation).WithOne(ed => ed.UserMaster).HasForeignKey(ed => ed.UserId);
        }
    }
}
