using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Utils
{
    public class IPValidationService
    {
        public static bool ValidateIPAddress(string ipAddress)
        {
            //ipAddress = "192.168.0.1";
            bool isValidIp = false;

            if (IPAddress.TryParse(ipAddress, out IPAddress ParsedIpAddress))
            {
                if (ParsedIpAddress.IsIPv4MappedToIPv6)
                {
                    // Es una dirección IPv4 mapeada a IPv6
                    isValidIp = true;
                }
                else if (ParsedIpAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    // Es una dirección IPv4 válida
                    isValidIp = true;
                }
                else if (ParsedIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    // Es una dirección IPv6 válida
                    isValidIp = true;
                }
            }
            return isValidIp;
        }
    }
}
