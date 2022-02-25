using Business.Shared.Consts;
using System.ComponentModel.DataAnnotations;

namespace Business.AppUserManagement.Dto
{
    public class UpdatePhoneRequestDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [RegularExpression("^[1][3-9]\\d{9}$", ErrorMessage = "{0}格式输入有误")]
        [StringLength(AppUserConsts.MaxPhoneLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Phone { get; set; }
    }
}
