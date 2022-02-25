using SharedLocker.Enums;
using SharedLocker.SharedLockers;
using System;
using System.Collections.Generic;
using UAP.Shared;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace SharedLocker.Domain.SharedLockers
{
    /// <summary>
    /// 储物箱
    /// </summary>]
    public class Locker : FullAuditedAggregateRoot<Guid>, IIsActive, IMultiTenant, IMultiApp
    {
        public Locker()
        {

        }

        public Locker(Guid id, Guid? tenantId, Guid? appId, string name, int number, bool isActive = true) : base(id)
        {
            SetTenantId(tenantId);
            SetAppId(appId);
            SetName(name);
            SetNumber(number);
            SetIsActive(isActive);
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get;protected set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Number { get;protected set; }

        /// <summary>
        /// 储物柜状态
        /// </summary>
        public LockerStatus Status { get;protected set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get;protected set; } = true;

        public Guid? AppId { get;protected set; }

        /// <summary>
        /// 出租记录
        /// </summary>
        public virtual ICollection<LockerRentInfo> RentInfos { get;protected set; } = new List<LockerRentInfo>();

        public Guid? TenantId { get; protected set; }

        #region 方法

        /// <summary>
        /// 关联机构
        /// </summary>
        /// <param name="tenantId"></param>
        public void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// 关联App
        /// </summary>
        /// <param name="appId"></param>
        public void SetAppId(Guid? appId)
        {
            AppId = appId;
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 设置编号
        /// </summary>
        /// <param name="number"></param>
        public void SetNumber(int number)
        {
            Number = number;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="active"></param>
        public void SetIsActive(bool active)
        {
            IsActive = active;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="lockerStatus"></param>
        public void SetStatus(LockerStatus lockerStatus)
        {
            Status = lockerStatus;
        }
        #endregion
    }
}
