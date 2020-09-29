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
                .ForMember(dest => dest.Component, opt => opt.MapFrom(src => src.Component.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Database.Entities.Device, DeviceForDetailDto>();

            CreateMap<Registration, RegistrationForListDto>()
                .ForMember(dest => dest.MacAddress, opt => opt.MapFrom(src => src.MacAddress.Label))
                .ForMember(dest => dest.BleAddress, opt => opt.MapFrom(src => src.BleAddress.Label));

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ComponentIds, opt => opt.MapFrom(src => src.Components.Select(x => x.Id).ToArray()));
        }
    }
}
