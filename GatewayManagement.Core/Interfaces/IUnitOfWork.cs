using GatewayManagementCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Gateway> Gateway { get; set; }
        public IRepository<PeripheralDevice> PeripheralDevice { get; set; }
        public Task Save();

    }
}
