using System;
using JetBrains.Annotations;
using UAP.Shared;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public class UserInfo : FullAuditedAggregateRoot<Guid>, IUserInfo, IMultiTenant, IMultiApp
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid? AppId { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        [CanBeNull]
        public virtual string NickName { get; protected set; }

        public virtual byte Gender { get; protected set; }
        
        [CanBeNull]
        public virtual string Language { get; protected set; }

        [CanBeNull]
        public virtual string City { get; protected set; }

        [CanBeNull]
        public virtual string Province { get; protected set; }

        [CanBeNull]
        public virtual string Country { get; protected set; }

        [CanBeNull]
        public virtual string AvatarUrl { get; protected set; }

        protected UserInfo()
        {
        }

        public UserInfo(Guid id,
            Guid? tenantId,
            Guid? appId,
            Guid userId,
            UserInfoModel model) : base(id)
        {
            TenantId = tenantId;
            AppId = appId;
            UserId = userId;

            UpdateInfo(model);
        }
        
        public void UpdateInfo(UserInfoModel model)
        {
            NickName = model.NickName;
            Gender = model.Gender;
            Language = model.Language;
            City = model.City;
            Province = model.Province;
            Country = model.Country;
            AvatarUrl = model.AvatarUrl;
        }
    }
}
