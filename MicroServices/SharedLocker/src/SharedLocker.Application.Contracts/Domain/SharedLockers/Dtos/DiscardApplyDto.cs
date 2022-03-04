using SharedLocker.Consts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    public class DiscardApplyDto
    {
        /// <summary>
        /// 作废原因
        /// </summary>
        [DisplayName("作废原因")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(LockerRentApplyConst.MaxDiscardReasonLength, ErrorMessage = "{0}不能超过{1}个字符长度")]
        public string Reason { get; set; }
    }
}
