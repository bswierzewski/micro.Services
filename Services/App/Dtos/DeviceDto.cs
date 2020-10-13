using System;
using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class DeviceDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }
        public DateTime? Created { get; set; }
        public string AddressLabel { get; set; }
        public int AddressId { get; set; }
        public Database.Entities.Address Address { get; set; }
        public int? CategoryId { get; set; }
        public Database.Entities.Category Category { get; set; }
        public int? KindId { get; set; }
        public Database.Entities.Kind Kind { get; set; }
        public int? ComponentId { get; set; }
        public Database.Entities.Component Component { get; set; }
        public int? VersionId { get; set; }
        public Database.Entities.Version Version { get; set; }
        public bool? IsAutoUpdate { get; set; }
    }
}
