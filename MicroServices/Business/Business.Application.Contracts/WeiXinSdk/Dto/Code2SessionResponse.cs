using Newtonsoft.Json;

namespace Business.WeiXinSdk.Dto
{
    public class Code2SessionResponse
    {
        public string ErrorMessage { get; set; }

        public int ErrorCode { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("session_key")]
        public string SessionKey { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
