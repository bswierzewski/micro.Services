using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Update.Dtos
{
    public class UploadDto
    {
        [Required]
        public short Major { get; set; }

        [Required]
        public short Minor { get; set; }

        [Required]
        public short Patch { get; set; }

        [Required]
        public short? TypeId { get; set; }

        [Required]
        public short? KindId { get; set; }

        [Required]
        public IFormFile File { get; set; }
        public string Name { get; set; }
    }
}