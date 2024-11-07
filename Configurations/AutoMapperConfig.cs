using AutoMapper;
using BaseWebApplication.Data;
using BaseWebApplication.Models;

namespace BaseWebApplication.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<DummyClass, DummyClassVM>()
                .ForMember(dest => dest.DummyClassType, opt => opt.MapFrom(src => src.DummyClassType));

            CreateMap<DummyClassVM, DummyClass>()
                .ForMember(dest => dest.DummyClassType, opt => opt.Ignore());


            CreateMap<AppUser, AppUserVM>().ReverseMap();
            CreateMap<AppUserConfig, AppUserConfigVM>().ReverseMap();
            CreateMap<DummyClassType, DummyClassTypeVM>().ReverseMap();

        }
    }
}
