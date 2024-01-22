using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MatchManager.Data.Context;

namespace MatchManager.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("DefaultSQLConnection");
            builder.UseSqlServer(connectionString);
            return new DataContext(builder.Options);
        }
    }
}
