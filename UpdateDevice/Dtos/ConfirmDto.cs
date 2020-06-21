using System.ComponentModel.DataAnnotations;

namespace UpdateDevice.Dtos
{
    public class ConfirmDto
    {
        [Required]

        public string MacAddress { get; set; }

        [Required]
        public int VersionId { get; set; }
    }
}
