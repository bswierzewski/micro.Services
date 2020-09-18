using System.ComponentModel.DataAnnotations;

namespace Update.Dtos
{
    public class ConfirmDto
    {
        [Required]

        public string Address { get; set; }

        [Required]
        public int? VersionId { get; set; }
    }
}
