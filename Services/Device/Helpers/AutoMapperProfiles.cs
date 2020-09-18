using AutoMapper;
using Device.Dtos;

namespace Device.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Database.Entities.Device, DeviceForListDto>();
        }
    }
}
