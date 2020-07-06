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

        public int ScannerDeviceId { get; set; }
        [ForeignKey("ScannerDeviceId")]
        public Device Device { get; set; }

        public string TrackDeviceMacAddress { get; set; }
        public int Rssi { get; set; }

    }
}