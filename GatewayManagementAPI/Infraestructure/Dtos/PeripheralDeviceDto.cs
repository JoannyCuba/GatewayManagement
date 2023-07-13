namespace GatewayManagementAPI.Infraestructure.Dtos
{
    public class PeripheralDeviceDto
    {
        public int? id { get; set; }
        public string? uId { get; set; }
        public string? vendor { get; set; }
        public DateTime dateCreated { get; set; }
        public bool isOnline { get; set; }
        public bool isActive { get; set; }
        public int? gatewayId { get; set; }
    }
}
