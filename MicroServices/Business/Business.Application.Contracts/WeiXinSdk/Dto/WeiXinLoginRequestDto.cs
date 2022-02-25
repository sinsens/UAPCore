using System.ComponentModel.DataAnnotations;

namespace Business.WeiXinSdk.Dto
{
    public class WeiXinLoginRequestDto
    {
        [Display(Name = "JsCode")]
        [Required(ErrorMessage = "参数{0}不能为空", AllowEmptyStrings = false)]
        public string JsCode { get; set; }
    }
}
