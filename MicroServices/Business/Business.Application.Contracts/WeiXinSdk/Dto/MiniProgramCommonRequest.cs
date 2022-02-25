using Newtonsoft.Json;

namespace Business.WeiXinSdk.Dto
{
    /// <summary>
    /// EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Models
    /// </summary>
    public abstract class MiniProgramCommonRequest : IMiniProgramRequest
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
