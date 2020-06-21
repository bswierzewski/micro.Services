using System.ComponentModel.DataAnnotations;

namespace UpdateDeviceService.Dtos
{
    public class ConfirmDto
    {
        [Required]

        public string MacAddress { get; set; }

        [Required]
        public int VersionId { get; set; }
    }
}
