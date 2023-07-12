namespace GatewayManagementAPI.Models
{
    public class PeripheralDevice : GatewayManagementCore.Entities.PeripheralDevice
    {
        public Gateway? Gateway { get; set; }
    }
}
