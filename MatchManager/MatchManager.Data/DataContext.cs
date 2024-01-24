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
        }
    }
}
