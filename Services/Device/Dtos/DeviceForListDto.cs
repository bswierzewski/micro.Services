namespace Device.Dtos
{
    public class DeviceForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public int? CategoryId { get; set; }
        public string Kind { get; set; }
        public int? KindId { get; set; }
        public string Component { get; set; }
        public int? ComponentId { get; set; }
    }
}