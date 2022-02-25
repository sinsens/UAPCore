using Business.WeiXinSdk;
using Business.WeiXinSdk.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Business.Controllers
{
    [Area("WeiXin")]
    [Route("api/app/WeiXin")]
    [RemoteService]
    public class WeiXinController:AbpController, IWeiXinAppService
    {
        private readonly IWeiXinAppService _weiXinAppService;

        public WeiXinController(IWeiXinAppService weiXinAppService)
        {
            _weiXinAppService = weiXinAppService;
        }

        /// <summary>
        /// 开始会话
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("initSession")]
        public ValueTask<Code2SessionResponseDto> Code2SessionAsync(WeiXinLoginRequestDto input)
        {
            return _weiXinAppService.Code2SessionAsync(input);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public ValueTask<string> LoginAsync(WeiXinUserProfileDto input)
        {
            return _weiXinAppService.LoginAsync(input);
        }
    }
}
