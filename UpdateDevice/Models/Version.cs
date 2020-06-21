using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpdateDeviceService.Models
{
    public class Version
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public short Major { get; set; }
        public short Minor { get; set; }
        public short Patch { get; set; }

        public int? FileDataId { get; set; }
        public FileData FileData { get; set; }
    }
}