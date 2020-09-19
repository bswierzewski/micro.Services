using AutoMapper;
using Device.Dtos;

namespace Device.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Database.Entities.Device, DeviceForListDto>()
                .ForMember(dest => dest.Kind, opt => opt.MapFrom(src => src.Kind.Name))
                .ForMember(dest => dest.DeviceComponent, opt => opt.MapFrom(src => src.DeviceComponent.Name));

            CreateMap<Database.Entities.Device, DeviceForDetailDto>();

            CreateMap<Database.Entities.Registration, RegistrationForListDto>();
        }
    }
}
