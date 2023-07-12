using GatewayManagementAPI.Data;
using GatewayManagementAPI.Infraestructure.Repositories;
using GatewayManagementCore.Entities;
using GatewayManagementCore.Interfaces;

namespace GatewayManagementAPI.Infraestructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRepository<Gateway> Gateway { get; set; }
        public IRepository<PeripheralDevice> PeripheralDevice { get; set; }
        public IRepository<AuditTrailManager> Audit { get; set ; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gateway = new GatewayRepository(context);
            PeripheralDevice = new PeripheralDeviceRepository(context);
            Audit = new AuditTrialRepository(context);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
