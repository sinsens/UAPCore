using Newtonsoft.Json;

namespace Business.WeiXinSdk.Dto
{
    public class WeiXinUserProfileDto
    {
        public string OpenId { get; set; }

        [JsonProperty("rawData")]
        public string RawData { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("encryptedData")]
        public string EncryptedData { get; set; }

        [JsonProperty("iv")]
        public string IV { get; set; }

        [JsonProperty("userInfo")]
        public WeiXinUserInfoDto UserInfo { get; set; }
    }

    public class WeiXinUserInfoDto
    {
        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }
    }
}
