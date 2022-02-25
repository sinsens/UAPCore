using Business.Shared.Consts;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Business.AppManagement.Dto
{
    public class CreateOrUpdateAppDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        [Display(Name = "应用名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(AppConsts.MaxAppNameLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string AppName { get; set; }

        /// <summary>
        /// App类型
        /// </summary>
        public AppType? AppType { get; set; }

        [Display(Name = "AppId")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(AppConsts.MaxAppIdLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string AppId { get; set; }


        [Display(Name = "AppSecret")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(AppConsts.MaxAppSecretLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string AppSecret { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
    }
}
