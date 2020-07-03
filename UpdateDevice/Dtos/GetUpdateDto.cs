using System.ComponentModel.DataAnnotations;

namespace UpdateDevice.Dtos
{
    public class GetUpdateDto
    {
        [Required]
        public string MacAddress { get; set; }
        [Required]
        public string DeviceType { get; set; }
    }
}
