using Database.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    /// <summary>
    /// Kind of devices 
    /// ESP32
    /// ESP8266
    /// etc.
    /// </summary>
    public class Kind
    {
        public short TypeId { get; set; } = (short)TypeEnum.Kind;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
    }
}
