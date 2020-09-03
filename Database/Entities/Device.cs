using Database.Entities.DeviceInfo;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        public bool? IsAutoUpdate { get; set; }
        public int? KindId { get; set; }
        public virtual Kind Kind { get; set; }
        public int? ComponentId { get; set; }
        public virtual Component Component { get; set; }

        public int? ActuallVersionId { get; set; }
        public int? SpecificVersionId { get; set; }

    }
}