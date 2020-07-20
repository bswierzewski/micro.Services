using System;

namespace Device.Data.DeviceInfo.Component
{
    public class GetComponentDto
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Created { get; internal set; }
        public int? CategoryId { get; internal set; }
        public string CategoryName { get; internal set; }
    }
}