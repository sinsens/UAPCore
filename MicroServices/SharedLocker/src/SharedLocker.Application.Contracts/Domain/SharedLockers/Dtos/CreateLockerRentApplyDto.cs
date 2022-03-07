using SharedLocker.Consts;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    [Serializable]
    public class CreateLockerRentApplyDto
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
        public DateTime RentTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(LockerRentConst.MaxLockerRentRemarkLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Remark { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        [DisplayName("申请数量")]
        [Required(ErrorMessage = "{0}是必填项")]
        [Range(1, 99, ErrorMessage = "{0}的范围应在{1}-{2}之间")]
        public int ApplyCount { get; set; }
    }
}