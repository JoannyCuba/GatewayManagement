using GatewayManagementCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Interfaces
{
    public interface IGatewayRepository
    {
        Task AddGateway(Gateway gateway);
        Task<Gateway> GetGateway(string serialNumber);
        Task<IEnumerable<Gateway>> GetAllGateways();
        Task RemoveGateway(string serialNumber);
    }
}
