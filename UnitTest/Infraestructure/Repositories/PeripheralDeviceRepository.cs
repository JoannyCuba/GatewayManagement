using GatewayManagementCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Infraestructure.Repositories
{
    public class PeripheralDeviceRepository : Repository<PeripheralDevice>
    {
        private DbSet<PeripheralDevice> dbSet;
        public PeripheralDeviceRepository(ApplicationDbContext context) : base(context)
        {
            dbSet = context.Set<PeripheralDevice>();
        }
        public async override Task<List<PeripheralDevice>> FindPaginationAsync(int page = 1, int itemPerPage = 25, Expression<Func<PeripheralDevice, bool>> filter = null)
        {
            IQueryable<PeripheralDevice> query = dbSet;
            query = query.Where(filter);
            query.Skip((page - 1) * itemPerPage).Take(itemPerPage);
            return await query.ToListAsync();
        }

        public async override Task<int> CountAsync(Expression<Func<PeripheralDevice, bool>> filter = null)
        {
            IQueryable<PeripheralDevice> query = dbSet;
            if (filter == null)
                return await query.CountAsync();
            return await query.Where(filter).CountAsync();
        }

        public async override Task<PeripheralDevice> FindOneAsync(Expression<Func<PeripheralDevice, bool>> filter = null)
        {
            IQueryable<PeripheralDevice> query = dbSet;
            return await query.FirstOrDefaultAsync(filter);
        }
        public override void Add(PeripheralDevice deviceCore)
        {
            dbSet.Add(deviceCore);
        }

        public async override void Update(PeripheralDevice deviceCore)
        {
            IQueryable<PeripheralDevice> query = dbSet;

            var device = query.FirstOrDefault(x => x.Id == deviceCore.Id);
            device.Id = deviceCore.Id;
            device.Vendor = deviceCore.Vendor;
            device.DateCreated = deviceCore.DateCreated;
            device.UID = deviceCore.UID;
            device.GatewayId = deviceCore.GatewayId;
            device.IsOnline = deviceCore.IsOnline;
            device.IsActive = deviceCore.IsActive;

            dbSet.Update(device);
        }
        public override void Remove(PeripheralDevice deviceCore)
        {
            IQueryable<PeripheralDevice> query = dbSet;
            var gateway = query.FirstOrDefault(x => x.Id == deviceCore.Id);
            if (gateway == null) return;
            dbSet.Remove(gateway);
        }
        public async override void Delete(Expression<Func<PeripheralDevice, bool>> filter = null)
        {
            IQueryable<PeripheralDevice> query = dbSet;

            var gateway = query.FirstOrDefault(filter);
            gateway.IsActive = false;
            dbSet.Update(gateway);
        }
    }
}
