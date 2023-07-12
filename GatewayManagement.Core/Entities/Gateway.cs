using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Entities
{
    public class Gateway
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public List<PeripheralDevice> PeripheralDevices { get; set; }
    }
}
