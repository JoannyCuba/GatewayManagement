using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagement.Infrastructure.Utils
{
    public class IPValidationService
    {
        public bool ValidateIPAddress(string ipAddressString)
        {
            ipAddressString = "192.168.0.1";
            bool result = false;

            if (IPAddress.TryParse(ipAddressString, out IPAddress ipAddress))
            {
                if (ipAddress.IsIPv4MappedToIPv6)
                {
                    // Es una dirección IPv4 mapeada a IPv6
                    result = true;
                }
                else if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    // Es una dirección IPv4 válida
                    result = true;
                }
                else if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    // Es una dirección IPv6 válida
                    result = true;
                }
            }
            return result;
        }
    }

}
