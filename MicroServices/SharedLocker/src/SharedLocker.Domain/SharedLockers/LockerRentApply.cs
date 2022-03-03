using UAP.Shared;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System;
using SharedLocker.Enums;
using SharedLocker.Domain.SharedLockers;

namespace SharedLocker.SharedLockers
{
    public class LockerRentApply : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiApp, IMultiTenant
    {
        /// <summary>
        /// 租用申请
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userId">userId</param>
        /// <param name="tenantId">机构id</param>
        /// <param name="appId">应用id</param>
        /// <param name="name">姓名</param>
        /// <param name="phone">联系电话</param>
        public LockerRentApply(Guid id, Guid userId, Guid? tenantId, Guid? appId, string name, string phone):base(id)
        {
            UserId = userId;
            TenantId = tenantId;
            AppId = appId;
            Name = name;
            Phone = phone;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 姓名拼音首字母
        /// </summary>
        public string PinyinName { get; set; }

        /// <summary>
        /// 姓名拼音全字母
        /// </summary>
        public string FullPinyinName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; protected set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// 申请租用时间
        /// </summary>
        public DateTime? RentTime { get; protected set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? AuditTime { get; protected set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string Auditor { get; protected set; }

        /// <summary>
        /// 审批备注
        /// </summary>
        public string AuditRemark { get; protected set; }

        public string DiscardReason { get; protected set; }

        public DateTime? DiscardTime { get; protected set; }

        public virtual Guid? LockerRentId { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        public virtual LockerRent LockerRent { get; protected set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public LockerRentApplyStatus Status { get; protected set; }

        public virtual Guid? AppId { get; protected set; }
        public virtual Guid? TenantId { get; protected set; }

        #region 方法

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="username">审核人</param>
        /// <param name="remark">备注</param>
        /// <param name="dateTime">审核时间</param>
        public void SetAudit(LockerRentApplyStatus status, string username, string remark, DateTime dateTime)
        {
            if (Status != LockerRentApplyStatus.Discard2)
            {
                Auditor = username;
                Status = status;
                AuditRemark = remark;
                AuditTime = dateTime;
            }
        }

        public void SetRentId(Guid lockerRentId)
        {
            LockerRentId = lockerRentId;
        }

        /// <summary>
        /// 设置姓名拼音
        /// </summary>
        /// <param name="pinyinName">简拼</param>
        /// <param name="fullPinyinName">全拼</param>
        public void SetPinyinName(string pinyinName, string fullPinyinName)
        {
            PinyinName = pinyinName;
            FullPinyinName = fullPinyinName;
        }

        /// <summary>
        /// 设置作废
        /// </summary>
        /// <param name="reason">作废原因</param>
        /// <param name="isApplier">是否申请人申请作废</param>
        public void SetDiscard(DateTime dateTime,string reason, bool isApplier)
        {
            if(Status != LockerRentApplyStatus.Discard && Status != LockerRentApplyStatus.Discard2)
            {
                DiscardTime = dateTime;
                Status = isApplier ? LockerRentApplyStatus.Discard2 : LockerRentApplyStatus.Discard;
                DiscardReason = reason;
            }
        }

        #endregion
    }
}
