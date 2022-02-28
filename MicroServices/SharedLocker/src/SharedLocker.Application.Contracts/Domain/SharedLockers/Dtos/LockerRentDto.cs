using SharedLocker.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    [Serializable]
    public class LockerRentDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 姓名拼音首字母
        /// </summary>
        public string PinyinName { get; set; }

        /// <summary>
        /// 姓名全部拼音
        /// </summary>
        public string FullPinyinName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 起租时间
        /// </summary>
        public DateTime? RentTime { get; set; }

        /// <summary>
        /// 妹子
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 租用状态
        /// </summary>
        public LockerRentStatus Status { get; set; }

        /// <summary>
        /// 租用状态
        /// </summary>
        public string StatusDesc { get; set; }

        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? ReturnTime { get; set; }

        /// <summary>
        /// 归还备注
        /// </summary>
        public string ReturnRemark { get; set; }

        /// <summary>
        /// 所属 App
        /// </summary>
        public Guid? AppId { get; set; }

        /// <summary>
        /// 储物柜
        /// </summary>
        public ICollection<LockerRentLockerDto> Lockers { get; set; } = new List<LockerRentLockerDto>();

        /// <summary>
        /// 日志
        /// </summary>
        public ICollection<LockerRentLogDto> Logs { get; set; } = new List<LockerRentLogDto>();
    }

    /// <summary>
    /// 储物柜
    /// </summary>
    public class LockerRentLockerDto : EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Number { get; set; }
    }

    /// <summary>
    /// 租用日志
    /// </summary>
    public class LockerRentLogDto
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}