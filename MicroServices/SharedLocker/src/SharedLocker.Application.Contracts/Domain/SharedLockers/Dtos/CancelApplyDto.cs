using SharedLocker.Consts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    public class CancelApplyDto
    {
        /// <summary>
        /// 取消原因
        /// </summary>
        [DisplayName("取消原因")]
        [StringLength(LockerRentApplyConst.MaxDiscardReasonLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Reason { get; set; }
    }
}
