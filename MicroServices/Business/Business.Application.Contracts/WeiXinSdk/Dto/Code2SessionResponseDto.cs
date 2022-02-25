
namespace Business.WeiXinSdk.Dto
{
    public class Code2SessionResponseDto
    {
        public string ErrorMessage { get; set; }

        public int ErrorCode { get; set; }
        public string OpenId { get; set; }
        public string SessionKey { get; set; }
        public string UnionId { get; set; }
    }
}
