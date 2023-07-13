using System.ComponentModel.DataAnnotations;

namespace GatewayManagementAPI.Infraestructure.Dtos
{
    public class GatewayDto
    {
        public int? id { get; set; }
        [Required(ErrorMessage = "The field serial number is required")]
        [StringLength(50, ErrorMessage = "The field serial number cannot exceed 50 characters")]
        public string serialNumber { get; set; }
        [MinLength(3, ErrorMessage = "The field name must have at least 3 caracters.")]
        [StringLength(50, ErrorMessage = "The field name cannot exceed 50 characters")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "The field name can only contains letters, numbers and spaces.")]
        public string? name { get; set; }
        [Required(ErrorMessage = "The field IP Address is required")]
        [StringLength(15, ErrorMessage = "The field IP Address cannot exceed 15 characters")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "IP address is not valid IPv4.")]
        public string ipAddress { get; set; }
        public List<PeripheralDeviceDto>? peripheralDevices { get; set; }
    }
}
