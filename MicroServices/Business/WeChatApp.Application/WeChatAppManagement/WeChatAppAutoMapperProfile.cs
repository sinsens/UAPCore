using AutoMapper;
using WeChatApp.WeChatAppManagement.Dto;
using WeChatApp.Models;

namespace WeChatApp.WeChatAppManagement
{
    public class WeChatAppAutoMapperProfile : Profile
    {
        public WeChatAppAutoMapperProfile()
        {
            CreateMap<WeChatApp, WeChatAppDto>();
            CreateMap<CreateOrUpdateWeChatAppDto, WeChatApp>();
        }
    }
}
