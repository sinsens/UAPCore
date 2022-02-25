using AutoMapper;
using Business.AppManagement.Dto;
using Business.Domain.UniApp;

namespace Business.AppManagement
{
    public class UniAppAutoMapperProfile : Profile
    {
        public UniAppAutoMapperProfile()
        {
            CreateMap<App, AppDto>();
            CreateMap<CreateOrUpdateAppDto, App>();
        }
    }
}
