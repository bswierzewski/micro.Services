using System;

namespace Device.Dtos
{
    public class DeviceComponentDto
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Created { get; internal set; }
        public int? CategoryId { get; internal set; }
        public string CategoryName { get; internal set; }
    }
}