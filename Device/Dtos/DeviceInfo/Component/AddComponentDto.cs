using System.ComponentModel.DataAnnotations;

namespace Device.Data.DeviceInfo.Component
{
    public class AddComponentDto
    {
        [Required]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
    }
}