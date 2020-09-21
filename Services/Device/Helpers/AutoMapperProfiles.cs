using AutoMapper;
using Device.Dtos;
using System.Linq;
using Database.Entities;

namespace Device.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Database.Entities.Device, DeviceForListDto>()
                .ForMember(dest => dest.Kind, opt => opt.MapFrom(src => src.Kind.Name))
                .ForMember(dest => dest.DeviceComponent, opt => opt.MapFrom(src => src.Component.Name));

            CreateMap<Database.Entities.Device, DeviceForDetailDto>();

            CreateMap<Registration, RegistrationForListDto>();

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.DeviceComponentIds, opt => opt.MapFrom(src => src.Components.Select(x => x.Id).ToArray()));
        }
    }
}
