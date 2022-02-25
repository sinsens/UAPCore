using SharedLocker.Consts;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    /// <summary>
    /// 归还
    /// </summary>
    public class ReturnLockerRentDto
    {
        [DisplayName("退租时间")]
        [Required(ErrorMessage = "{0}是必填项")]
        public DateTime ReturnTime { get; set; }


        [DisplayName("归还备注")]
        [StringLength(LockerRentConst.MaxLockerRentReturnRemarkLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string ReturnRemark { get; set; }
    }
}
