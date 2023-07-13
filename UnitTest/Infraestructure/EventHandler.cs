using GatewayManagementCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Infraestructure
{
    public class EventHandler : IEventHandler
    {
        public void ThrowEvent(string _event, params object[] _args)
        {
            Console.Write(_event, _args);
        }
    }
}
