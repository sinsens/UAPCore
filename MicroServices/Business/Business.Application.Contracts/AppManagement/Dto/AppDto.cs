using System;
using Volo.Abp.Application.Dtos;

namespace Business.AppManagement.Dto
{
    public class AppDto : EntityDto<Guid>
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// App类型
        /// </summary>
        public AppType? AppType { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
    }
}
