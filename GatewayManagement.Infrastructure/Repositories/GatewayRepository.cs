using GatewayManagement.Core.Interfaces;
using GatewayManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagement.Infrastructure.Repositories
{
    public class GatewayRepository : IGatewayRepository
    {
        private readonly YourDbContext dbContext;

        public GatewayRepository(YourDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddGateway(Gateway gateway)
        {
            dbContext.Gateways.Add(gateway);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Gateway> GetGateway(string serialNumber)
        {
            return await dbContext.Gateways.FirstOrDefaultAsync(g => g.SerialNumber == serialNumber);
        }

        public async Task<IEnumerable<Gateway>> GetAllGateways()
        {
            return await dbContext.Gateways.ToListAsync();
        }

        public async Task RemoveGateway(string serialNumber)
        {
            var gateway = await dbContext.Gateways.FirstOrDefaultAsync(g => g.SerialNumber == serialNumber);
            if (gateway != null)
            {
                dbContext.Gateways.Remove(gateway);
                await dbContext.SaveChangesAsync();
            }
        }
    }

}
