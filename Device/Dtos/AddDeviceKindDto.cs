using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class AddDeviceKindDto
    {
        [Required]
        public string Kind { get; set; }
    }
}
