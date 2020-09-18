using Database.Entities.DeviceInfo;
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

        public int? DeviceComponentId { get; set; }
        public virtual DeviceComponent DeviceComponent { get; set; }

        public int? KindId { get; set; }
        public virtual Kind Kind { get; set; }

        public int FileDataId { get; set; }
        public virtual FileData FileData { get; set; }
    }
}