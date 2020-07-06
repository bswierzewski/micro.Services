using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class PostDeviceKindDto
    {
        [Required]
        public string Kind { get; set; }
    }
}
