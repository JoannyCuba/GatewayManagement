using GatewayManagementCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeripheralDevice>()
               .HasOne(i => i.Gateway)
               .WithMany(a => a.PeripheralDevices)
               .HasForeignKey(i => i.GatewayId);
        }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<PeripheralDevice> PeripheralDevices { get; set; }
        public DbSet<AuditTrailManager> AuditTrailManagers { get; set; }

    }
}
