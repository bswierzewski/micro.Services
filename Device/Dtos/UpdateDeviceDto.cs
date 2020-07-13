namespace Device.Dtos
{
    public class UpdateDeviceDto
    {
        public string Name { get; set; }
        public short? TypeId { get; set; }
        public short? KindId { get; set; }
        public int? VersionId { get; set; }
        public bool? IsAutoUpdate { get; set; }
    }
}
