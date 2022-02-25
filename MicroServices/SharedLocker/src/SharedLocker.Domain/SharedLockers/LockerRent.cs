using JetBrains.Annotations;
using SharedLocker.SharedLockers;
using System;
using System.Collections.Generic;
using UAP.Shared;
using UAP.Shared.Helper;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

using System.Linq;
using SharedLocker.Enums;

namespace SharedLocker.Domain.SharedLockers
{
    /// <summary>
    /// 储物柜租用信息
    /// </summary>
    public class LockerRent : FullAuditedAggregateRoot<Guid>, IMultiApp, IMultiTenant
    {
        public LockerRent()
        {

        }
        public LockerRent(Guid id, Guid? tenantId, Guid? appId, string name, string phone, string remark, DateTime? rentTime)
            : base(id)
        {
            SetTenantId(tenantId);
            SetAppId(appId);
            SetName(name);
            SetPhone(phone);
            SetRemark(remark);
            SetRentTime(rentTime);
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Name { get; set; }

        public Guid? TenantId { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 租用开始时间
        /// </summary>
        public DateTime? RentTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 租用状态
        /// </summary>
        public LockerRentStatus Status { get; set; }

        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? ReturnTime { get; set; }

        /// <summary>
        /// 归还备注
        /// </summary>
        public string ReturnRemark { get; set; }

        public Guid? AppId { get; set; }

        /// <summary>
        /// 出租信息
        /// </summary>
        public virtual ICollection<LockerRentInfo> RentInfos { get; set; } = new List<LockerRentInfo>();

        /// <summary>
        /// 日志信息
        /// </summary>
        public virtual ICollection<LockerRentLog> Logs { get; set; } = new List<LockerRentLog>();

        #region 方法

        /// <summary>
        /// 设置机构 ID
        /// </summary>
        /// <param name="tenantId"></param>
        public void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// 设置 AppID
        /// </summary>
        /// <param name="appId"></param>
        public void SetAppId(Guid? appId)
        {
            AppId = appId;
        }

        /// <summary>
        /// 设置联系人
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 设置联系电话
        /// </summary>
        /// <param name="phone"></param>
        public void SetPhone(string phone)
        {
            Phone = phone;
        }

        /// <summary>
        /// 设置起租时间
        /// </summary>
        /// <param name="rentDateTime"></param>
        public void SetRentTime(DateTime? rentDateTime)
        {
            RentTime = rentDateTime;
        }

        /// <summary>
        /// 设置备注
        /// </summary>
        /// <param name="remark"></param>
        public void SetRemark(string remark)
        {
            Remark = remark;
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="returnTime"></param>
        /// <param name="remark"></param>
        public void SetReturn([NotNull] DateTime returnTime, string remark)
        {
            if (Status == LockerRentStatus.InService)
            {
                Status = LockerRentStatus.EndService;
                ReturnTime = ReturnTime ?? DateTime.Now;
                ReturnRemark = remark;
            }
        }

        /// <summary>
        /// 作废
        /// </summary>
        public void SetDiscard()
        {
            Status = LockerRentStatus.Discard;
        }

        public void SetLockers(IEnumerable<Guid> lockerIds)
        {
            foreach (var lockerId in lockerIds)
            {
                if (!RentInfos.Any(x => x.LockerId == lockerId))
                {
                    RentInfos.Add(new LockerRentInfo(GuidHelper.GetSequentialGuid(), lockerId));
                }
            }
        }

        public void AddLog(string username, string action, string description)
        {
            Logs.Add(new LockerRentLog(GuidHelper.GetSequentialGuid(), username, action, description));
        }
        #endregion
    }
}
