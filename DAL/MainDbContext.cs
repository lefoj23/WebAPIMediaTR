using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class MainDbContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public MainDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<BalanceHistory> BalanceHistory { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

    }
}