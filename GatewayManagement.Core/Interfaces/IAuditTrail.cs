using GatewayManagementCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Interfaces
{
    public interface IAuditTrail
    {
        public Task SaveAuditTrail(AuditTrailManager audit);
    }
}
