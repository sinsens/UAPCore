using SharedLocker.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    [Serializable]
    public class LockerDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public LockerStatus Status { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDesc { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 关联应用
        /// </summary>
        public Guid? AppId { get; set; }

        /// <summary>
        /// 出租记录
        /// </summary>
        //public ICollection<LockerRentInfoDto> RentInfos { get; set; } = new List<LockerRentInfoDto>();

    }

    /// <summary>
    /// 出租记录
    /// </summary>
    public class LockerRentInfoDto
    {
        /// <summary>
        /// 出租ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 出租时间
        /// </summary>
        public DateTime? RentTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? ReturnTime { get; set; }

        /// <summary>
        /// 归还备注
        /// </summary>
        public string ReturnRemark { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public LockerRentStatus Status { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDesc { get; set; }
    }
}