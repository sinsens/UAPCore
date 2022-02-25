using System;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Business.Domain.UniApp
{
    /// <summary>
    /// App
    /// </summary>
    [Audited]
    public class App : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant, IIsActive
    {
        public App()
        {

        }

        /// <summary>
        /// App
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appName">App 名称</param>
        /// <param name="appId">App Id</param>
        /// <param name="appSecret">App Secret</param>
        /// <param name="appType">App 类型</param>
        /// <param name="isActive">是否激活：是</param>
        public App(Guid id, Guid? tenantId, string appName, string appId, string appSecret, AppType? appType = null, bool isActive = true) : base(id)
        {
            SetTenantId(tenantId);
            SetAppName(appName);
            SetAppId(appId);
            SetAppSecret(appSecret);
            SetAppType(appType);
            SetIsActive(isActive);
        }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; protected set; }

        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; protected set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; protected set; }

        /// <summary>
        /// App类型
        /// </summary>
        public AppType? AppType { get; protected set; }

        public Guid? TenantId { get; protected set; }

        public bool IsActive { get; protected set; } = true;

        #region 方法

        public void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        public void SetAppName(string appName)
        {
            AppName = Check.NotNullOrWhiteSpace(appName, nameof(appName));
        }

        public void SetAppId(string appId)
        {
            AppId = appId;
        }

        public void SetAppSecret(string appSecret)
        {
            AppSecret = appSecret;
        }

        public void SetAppType(AppType? appType)
        {
            AppType = appType;
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }
        #endregion
    }
}