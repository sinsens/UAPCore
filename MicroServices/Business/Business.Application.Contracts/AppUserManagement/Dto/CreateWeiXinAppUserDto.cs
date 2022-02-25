using Business.Shared.Consts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.AppUserManagement.Dto
{
    /// <summary>
    /// 创建或更新 AppUser
    /// </summary>
    public class CreateWeiXinAppUserDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(AppUserConsts.MaxNameLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [RegularExpression("^[1][3-9]\\d{9}$", ErrorMessage = "{0}格式输入有误")]
        [StringLength(AppUserConsts.MaxPhoneLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Phone { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        [StringLength(AppUserConsts.MaxAddressLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Address { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        [Display(Name = "所在城市")]
        [StringLength(AppUserConsts.MaxCityLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string City { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        [Display(Name = "OpenId")]
        [StringLength(AppUserConsts.MaxOpenIdLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string OpenId { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "生日")]
        public DateTime? Birth { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public GenderEnum? Gender { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public string UserInfoData { get; set; }
    }
}
