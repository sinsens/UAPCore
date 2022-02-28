using System;
using System.ComponentModel;

namespace SharedLocker.Enums
{
    /// <summary>
    /// 储物柜状态
    /// </summary>
    public enum LockerStatus
    {
        /// <summary>
        /// 闲置
        /// </summary>
		[Description("闲置")]
        Idle = 0,

        /// <summary>
        /// 使用中
        /// </summary>
		[Description("使用中")]
        Busy = 1,

        /// <summary>
        /// 维护中
        /// </summary>
		[Description("维护中")]
        Maintenance = 2,
    }

    /// <summary>
    /// 租用状态
    /// </summary>
    public enum LockerRentStatus
    {
        /// <summary>
        /// 租用中
        /// </summary>
		[Description("租用中")]
        InService = 1,

        /// <summary>
        /// 已结束
        /// </summary>
		[Description("已结束")]
        EndService = 8,

        /// <summary>
        /// 作废
        /// </summary>
		[Description("已作废")]
        Discard = 10,
    }
}
