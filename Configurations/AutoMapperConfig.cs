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
        }
    }
}
