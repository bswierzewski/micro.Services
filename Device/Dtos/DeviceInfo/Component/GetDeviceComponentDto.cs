using System;

namespace Device.Data.DeviceInfo.DeviceComponent
{
    public class GetDeviceComponentDto
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Created { get; internal set; }
        public int? CategoryId { get; internal set; }
        public string CategoryName { get; internal set; }
    }
}