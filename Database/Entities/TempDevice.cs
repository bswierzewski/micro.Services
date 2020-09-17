using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class TempDevice
    {
        public short TypeId { get; set; } = (short)Enums.TempDeviceType.undefined;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string MacAddress { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
