using GatewayManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GatewayManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration["Database:SqlServerConnection"]);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        
        }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<PeripheralDevice> PeripheralDevices { get; set; }
    }
}
