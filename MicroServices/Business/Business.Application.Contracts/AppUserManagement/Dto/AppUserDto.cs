using Business.Shared.Consts;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Business.AppUserManagement.Dto
{
    public class AppUserDto : EntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
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
    }
}
