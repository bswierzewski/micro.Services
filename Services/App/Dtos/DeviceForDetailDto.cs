using Database.Entities;
using System;

namespace Device.Dtos
{
    public class DeviceForDetailDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public Kind Kind { get; set; }
        public Category Category { get; set; }
        public Component Component { get; set; }
        public Database.Entities.Version Version { get; set; }
        public Address Address { get; set; }

    }
}
