using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Address
    {
        public short TypeId { get; set; } = (short)Enums.AddressType.undefined;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Label { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
