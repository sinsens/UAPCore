using SharedLocker.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    public class LockerRentApplyDto : EntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get;  set; }

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
        public string Phone { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 申请租用时间
        /// </summary>
        public DateTime? RentTime { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string Auditor { get; set; }

        /// <summary>
        /// 审批备注
        /// </summary>
        public string AuditRemark { get; set; }

        public string DiscardReason { get; set; }

        public DateTime? DiscardTime { get; set; }

        public string CancelReason { get; set; }

        public DateTime? CancelTime { get; set; }

        public virtual Guid? LockerRentId { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual LockerRentDto LockerRent { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public LockerRentApplyStatus Status { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public string StatusDesc { get; set; }
		
		/// <summary>
		/// 申请时间
		/// </summary>
		public DateTime CreationTime {get;set;}
    }
}
