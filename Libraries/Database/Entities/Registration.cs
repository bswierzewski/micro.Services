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

        [ForeignKey("ScannerAddress")]
        public int ScannerAddressId { get; set; }
        public Address ScannerAddress { get; set; }

        [ForeignKey("TrackerAddress")]
        public int TrackerAddressId { get; set; }
        public Address TrackerAddress { get; set; }
        public int Rssi { get; set; }

    }
}