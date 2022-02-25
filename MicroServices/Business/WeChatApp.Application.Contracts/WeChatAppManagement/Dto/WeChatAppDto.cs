using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace WeChatApp.WeChatAppManagement.Dto
{
    public class WeChatAppDto : EntityDto<Guid?>
    {
        
        /// <summary>
        /// AppID
        /// </summary>
        [Required]
        public string AppId { get; set; }
        
        /// <summary>
        /// 应用名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// AppSecret
        /// </summary>
        [Required]
        public string AppSecret { get; set; }
        
        /// <summary>
        /// 数据密钥
        /// </summary>
        public string EncodingAesKey { get; set; }
        
        /// <summary>
        /// 显示应用名称
        /// </summary>
        [Required]
        public string DisplayName { get; set; }
        
    }
}