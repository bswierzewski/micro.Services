using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Version
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public short Major { get; set; }
        public short Minor { get; set; }
        public short Patch { get; set; }

        public short? DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }

        public short? DeviceKindId { get; set; }
        public DeviceKind DeviceKind { get; set; }

        public int FileDataId { get; set; }
        public FileData FileData { get; set; }
    }
}