using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace UpdateDevice.Dtos
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
        public short? DeviceTypeId { get; set; }

        [Required]
        public short? DeviceKindId { get; set; }

        [Required]
        public IFormFile File { get; set; }
        public string Name { get; set; }
    }
}