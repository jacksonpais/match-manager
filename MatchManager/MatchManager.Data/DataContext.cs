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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
