using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace WeChatApp.Models
{
    /// <summary>
    /// 新建App
    /// </summary>
    public class WeChatApp: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
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
        
		
		public bool IsDeleted { get; set; }
    }
}