using Microsoft.EntityFrameworkCore;

namespace money.data.Seeds
{
    public static class SeedConfiguration
    {
        public static void AddSeedData(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AccountTypeMaster>().HasData(new AccountTypeSeedData().AddSeedData());
        }
    }
}
