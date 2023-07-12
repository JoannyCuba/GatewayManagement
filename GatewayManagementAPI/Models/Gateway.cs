namespace GatewayManagementAPI.Models
{
    public class Gateway : GatewayManagementCore.Entities.Gateway
    {
        public List<PeripheralDevice> PeripheralDevices { get; set; }
    }
}
