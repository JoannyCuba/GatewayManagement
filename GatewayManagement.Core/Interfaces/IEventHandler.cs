using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Interfaces
{
    public interface IEventHandler
    {
        public void ThrowEvent(string _event, params object[] _args);
    }
}
