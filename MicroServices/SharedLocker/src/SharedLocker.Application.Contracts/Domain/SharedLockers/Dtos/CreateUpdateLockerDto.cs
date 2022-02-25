using SharedLocker.Consts;
using SharedLocker.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    [Serializable]
    public class CreateUpdateLockerDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(LockerConst.MaxLockerNameLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        [Required(ErrorMessage = "{0}是必填项")]
        [Range(LockerConst.MinLockerNumber, LockerConst.MaxLockerNumber, ErrorMessage = "{0}应在{1}-{2}之间")]
        public int Number { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public LockerStatus? Status { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>

        [DisplayName("是否启用")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 关联应用
        /// </summary>
        [DisplayName("关联应用")]
        [Required(ErrorMessage = "{0}是必填项")]
        public Guid? AppId { get; set; }

    }
}