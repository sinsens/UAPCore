using Business.WeiXinSdk.Dto;
using System.Threading.Tasks;

namespace Business.WeiXinSdk
{
    public interface IWeiXinAppService
    {
        /// <summary>
        /// 登录凭证校验。通过 wx.login 接口获得临时登录凭证 code 后传到开发者服务器调用此接口完成登录流程。
        /// </summary>
        /// <param name="appId">小程序 appId</param>
        /// <param name="appSecret">小程序 appSecret</param>
        /// <param name="jsCode">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        ValueTask<Code2SessionResponseDto> Code2SessionAsync(WeiXinLoginRequestDto input);

        /// <summary>
        /// 登录并获取 AccessToken
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<string> LoginAsync(WeiXinUserProfileDto input);
    }
}
