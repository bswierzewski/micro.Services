using System;

namespace Device.Dtos
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string IconName { get; set; }


        public int AddressId { get; set; }
        public bool? IsAutoUpdate { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public int? KindId { get; set; }
        public string Kind { get; set; }
        public int? DeviceComponentId { get; set; }
        public string DeviceComponent { get; set; }
        public int? ActuallVersionId { get; set; }
        public int? SpecificVersionId { get; set; }
    }
}