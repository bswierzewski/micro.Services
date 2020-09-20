using AutoMapper;
using Device.Dtos;
using System.Linq;

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

            CreateMap<Database.Entities.DeviceInfo.Category, CategoryDto>()
                .ForMember(dest => dest.DeviceComponentIds, opt => opt.MapFrom(src => src.DeviceComponents.Select(x => x.Id).ToArray()));
        }
    }
}
