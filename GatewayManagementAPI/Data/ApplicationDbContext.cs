using GatewayManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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
            modelBuilder.Entity<PeripheralDevice>()
               .HasOne(i => i.Gateway)
               .WithMany(a => a.PeripheralDevices)
               .HasForeignKey(i => i.GatewayId);

            modelBuilder.Entity<Gateway>()
               .ToTable("Gateway");

            modelBuilder.Entity<PeripheralDevice>()
               .ToTable("PeripheralDevice");
        }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<PeripheralDevice> PeripheralDevices { get; set; }
    }
}
