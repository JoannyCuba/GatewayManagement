namespace GatewayManagementAPI.Models
{
    public class PeripheralDevice : GatewayManagementCore.Entities.PeripheralDevice
    {
        public int GatewayId { get; set; }
        public Gateway Gateway { get; set; }
    }
}
