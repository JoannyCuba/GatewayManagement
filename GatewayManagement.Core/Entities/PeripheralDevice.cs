using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Entities
{
    public class PeripheralDevice
    {
        public int UID { get; set; }
        public string Vendor { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsOnline { get; set; }
        public int GatewayId { get; set; }

    }
}
