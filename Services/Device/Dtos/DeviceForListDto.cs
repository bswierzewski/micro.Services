namespace Device.Dtos
{
    public class DeviceForListDto
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int? CategoryId { get; set; }
        public int? KindId { get; set; }
        public int? DeviceComponentId { get; set; }
    }
}