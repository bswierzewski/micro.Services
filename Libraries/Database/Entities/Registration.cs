using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("MacAddress")]
        public int MacAddressId { get; set; }
        public Address MacAddress { get; set; }

        [ForeignKey("BleAddress")]
        public int BleAddressId { get; set; }
        public Address BleAddress { get; set; }
        public int Rssi { get; set; }

    }
}