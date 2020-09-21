using AutoMapper;
using Update.Dtos;

namespace Update.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Database.Entities.Version, VersionForListDto>()
                .ForMember(dest => dest.Kind, opt => opt.MapFrom(src => src.Kind.Name))
                .ForMember(dest => dest.DeviceComponent, opt => opt.MapFrom(src => src.Component.Name))
                .ForMember(dest => dest.FileData, opt => opt.MapFrom(src => $"{src.FileData.Name}.{src.FileData.Extension}"))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToString("yyyy-MM-dd HH:mm")));

        }
    }
}
