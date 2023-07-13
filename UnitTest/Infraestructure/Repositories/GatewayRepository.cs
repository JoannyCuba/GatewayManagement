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
    public class GatewayRepository : Repository<Gateway>
    {
        private DbSet<Gateway> dbSet;
        public GatewayRepository(ApplicationDbContext context) : base(context)
        {
            dbSet = context.Set<Gateway>();
        }
        public async override Task<List<Gateway>> FindPaginationAsync(int page = 1, int itemPerPage = 25, Expression<Func<Gateway, bool>> filter = null)
        {
            IQueryable<Gateway> query = dbSet;
            query = query.Where(filter);
            query = query.Include(x => x.PeripheralDevices.Where(d => d.IsActive == true));
            query.Skip((page - 1) * itemPerPage).Take(itemPerPage);
            return await query.ToListAsync();
        }

        public async override Task<int> CountAsync(Expression<Func<Gateway, bool>> filter = null)
        {
            IQueryable<Gateway> query = dbSet;
            if (filter == null)
                return await query.CountAsync();
            return await query.Where(filter).CountAsync();
        }

        public async override Task<Gateway> FindOneAsync(Expression<Func<Gateway, bool>> filter = null)
        {
            IQueryable<Gateway> query = dbSet;
            query = query.Include(x => x.PeripheralDevices);
            return await query.FirstOrDefaultAsync(filter);
        }
        public override void Add(Gateway gatewayCore)
        {
            dbSet.Add(gatewayCore);
        }

        public async override void Update(Gateway gatewayCore)
        {
            IQueryable<Gateway> query = dbSet;

            var gateway = query.FirstOrDefault(x => x.Id == gatewayCore.Id);
            gateway.Id = gatewayCore.Id;
            gateway.SerialNumber = gatewayCore.SerialNumber;
            gateway.Name = gatewayCore.Name;
            gateway.IPAddress = gatewayCore.IPAddress;
            gateway.IsActive = gatewayCore.IsActive;

            dbSet.Update(gateway);
        }
        public override void Remove(Gateway gatewayCore)
        {
            IQueryable<Gateway> query = dbSet;
            var gateway = query.FirstOrDefault(x => x.Id == gatewayCore.Id);
            if (gateway == null) return;
            dbSet.Remove(gateway);
        }
        public async override void Delete(Expression<Func<Gateway, bool>> filter = null)
        {
            IQueryable<Gateway> query = dbSet;

            var gateway = query.FirstOrDefault(filter);
            gateway.IsActive = false;
            dbSet.Update(gateway);
        }
    }
}
