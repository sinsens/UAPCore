using AutoMapper;
using Business.AppUserManagement.Dto;
using Business.Domain.UniApp;

namespace Business.AppUserManagement
{
    public class AppUserAutoMapperProfile : Profile
    {
        public AppUserAutoMapperProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<CreateOrUpdateAppUserDto, AppUser>();
            CreateMap<CreateWeiXinAppUserDto, AppUser>();
        }
    }
}
