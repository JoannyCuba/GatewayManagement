using GatewayManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagement.Core.Interfaces
{
    public interface IPeripheralDeviceRepository
    {
        Task AddPeripheralDevice(string gatewaySerialNumber, PeripheralDevice device);
        Task RemovePeripheralDevice(string gatewaySerialNumber, int deviceUID);
    }
}
