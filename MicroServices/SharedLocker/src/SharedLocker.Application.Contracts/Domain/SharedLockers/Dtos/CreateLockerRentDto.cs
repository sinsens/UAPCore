using SharedLocker.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    [Serializable]
    public class CreateLockerRentDto
    {
        /// <summary>
        /// 租用人
        /// </summary>
        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(LockerRentConst.MaxLockerRentNameLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Name { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(LockerRentConst.MaxLockerRentPhoneLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Phone { get; set; }

        /// <summary>
        /// 起租时间
        /// </summary>
        [DisplayName("起租时间")]
        [Required(ErrorMessage = "{0}是必填项")]
        public DateTime? RentTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(LockerRentConst.MaxLockerRentRemarkLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Remark { get; set; }

        /// <summary>
        /// 关联应用
        /// </summary>
        [DisplayName("关联应用")]
        [Required(ErrorMessage = "{0}是必填项")]
        public Guid? AppId { get; set; }

        public IEnumerable<Guid> LockerIds { get; set; } = new List<Guid>();
    }
}