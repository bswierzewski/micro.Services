using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Update.Dtos
{
    public class UploadDto
    {
        public string Name { get; set; }

        [Required]
        public short? Major { get; set; }

        [Required]
        public short? Minor { get; set; }

        [Required]
        public short? Patch { get; set; }

        [Required]
        public int? KindId { get; set; }

        [Required]
        public int? ComponentId { get; set; }

        public IFormFile File { get; set; }
    }
}