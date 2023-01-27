using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MSRCP_Server.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var connectionString = builder.Build().GetSection("ConnectionStrings").GetSection("MSRCP").Value;
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkData> WorkDatas { get; set; }
    }
}
