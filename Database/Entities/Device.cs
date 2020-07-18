using Database.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Device
    {
        public short TypeId { get; set; } = (short)TypeEnum.Device;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        public short? KindTypeId { get; set; } = (short)TypeEnum.Kind;
        public int? KindId { get; set; }
        public short? ComponentTypeId { get; set; } = (short)TypeEnum.Component;
        public int? ComponentId { get; set; }

        public int? ActuallVersionId { get; set; }
        public int? SpecificVersionId { get; set; }

    }
}