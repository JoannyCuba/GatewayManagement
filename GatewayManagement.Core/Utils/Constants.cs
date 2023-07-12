using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Utils
{
    public static class Constants
    {
        public static class Events
        {
            //Generales
            public const string UnexpectedError = "Unexpected Error ";
            public const string Information = "Information";

            //Gateways
            public const string ListGateways = "List Gateways";
            public const string GetGatewayById = "Get Gateway By Id";
            public const string CreateGateway = "Create Gateway";
            public const string UpdateGateway = "Update Gateway";
            public const string DeleteGateway = "Delete Gateway";

            //Peripheral Device
            public const string ListPeripheralDevices = "List Peripheral Device";
            public const string GetPeripheralDeviceById = "Get Peripheral Device";
            public const string CreatePeripheralDevice = "Create Peripheral Device";
            public const string UpdatePeripheralDevice = "Update Peripheral Device";
            public const string DeletePeripheralDevice = "Delete Gateways";


        }
        public static class GeneralErrors
        {
            public const string UnexpectedError = "Unexpected Error happens, if it persists, contact to your administrator";
        }

        public static class AuditTrailState
        {
            public const string Completed = "Completed";
            public const string Error = "Error";
            public const string NotFound = "Not Found";
        }

    }
}
