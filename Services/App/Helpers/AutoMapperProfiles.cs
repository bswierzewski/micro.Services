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
            CreateMap<Database.Entities.Device, DeviceDto>();

            CreateMap<Registration, RegistrationForListDto>();

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ComponentIds, opt => opt.MapFrom(src => src.Components.Select(x => x.Id).ToArray()));

            CreateMap<Component, ComponentDto>();
        }
    }
}
