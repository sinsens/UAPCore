using Newtonsoft.Json;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Business.Domain.UniApp
{
    /// <summary>
    /// AppUser
    /// </summary>
    public class AppUser : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant, IIsActive, IMultiApp
    {
        public AppUser()
        {

        }

        /// <summary>
        /// AppUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantId">tenantId</param>
        /// <param name="appId">appId</param>
        /// <param name="openId">openId</param>
        /// <param name="name">姓名</param>
        /// <param name="phone">电话</param>
        public AppUser(Guid id, Guid? tenantId, Guid? appId, string openId) : base(id)
        {
            SetTenantId(tenantId);
            SetAppId(appId);
            SetOpenId(openId);
        }

        /// <summary>
        /// AppUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantId">tenantId</param>
        /// <param name="appId">appId</param>
        /// <param name="openId">openId</param>
        /// <param name="name">姓名</param>
        /// <param name="phone">电话</param>
        public AppUser(Guid id, Guid? tenantId, Guid? appId, string openId, string name, string phone):base(id)
        {
            SetTenantId(tenantId);
            SetAppId(appId);
            SetOpenId(openId);
            SetName(name);
            SetPhone(phone);
        }

        public string Name { get; protected set; }

        public string Phone { get; protected set; }

        public string Address { get; protected set; }

        public string City { get; protected set; }        

        public string OpenId { get; protected set; }

        public string UnionId { get; protected set; }

        public DateTime? Birth { get; protected set; }

        public GenderEnum? Gender { get; protected set; }

        public Guid? TenantId { get; protected set; }

        public Guid? AppId { get; protected set; }

        [JsonProperty("session_key")]
        public string SessionKey { get; protected set; }

        public virtual App App { get; protected set; }

        public bool IsActive { get; protected set; } = true;

        #region 方法

        public void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        public void SetAppId(Guid? appId)
        {
            TenantId = appId;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPhone(string phone)
        {
            Phone = phone;
        }

        public void SetAdress(string address)
        {
            Address = address;
        }

        public void SetCity(string city)
        {
            City = city;
        }

        public void SetOpenId(string openId)
        {
            OpenId = openId;
        }

        public void SetUnionId(string unionId)
        {
            UnionId = unionId;
        }

        public void SetBirth(DateTime? birth)
        {
            Birth = birth;
        }

        public void SetGender(GenderEnum? gender)
        {
            Gender = gender;
        }
        #endregion
    }
}
