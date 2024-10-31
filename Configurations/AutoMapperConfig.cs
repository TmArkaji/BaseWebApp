using AutoMapper;
using BaseWebApplication.Data;
using BaseWebApplication.Models;
using System.Diagnostics.Metrics;

namespace BaseWebApplication.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AppUser, AppUserVM>().ReverseMap();

            CreateMap<AppUserConfig, AppUserConfigVM>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser));

            CreateMap<AppUserConfigVM, AppUserConfig>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser));
        }
    }
}
