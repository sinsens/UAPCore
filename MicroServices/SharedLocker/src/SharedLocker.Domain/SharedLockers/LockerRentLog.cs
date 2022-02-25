using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace SharedLocker.Domain.SharedLockers
{
    /// <summary>
    /// 出租日志
    /// </summary>
    public class LockerRentLog : CreationAuditedEntity<Guid>
    {
        public LockerRentLog()
        {

        }
        /// <summary>
        /// 出租日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username">操作人</param>
        /// <param name="action">动作</param>
        /// <param name="description">描述</param>
        public LockerRentLog(Guid id, string username, string action, string description) : base(id)
        {
            Username = username;
            Action = action;
            Description = description;
        }

        /// <summary>
        /// 动作
        /// </summary>
        public string Username { get; protected set; }

        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; protected set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// 出租记录ID
        /// </summary>
        public Guid? LockerRentId { get; protected set; }

        public virtual LockerRent LockerRent { get; protected set; }
    }
}
