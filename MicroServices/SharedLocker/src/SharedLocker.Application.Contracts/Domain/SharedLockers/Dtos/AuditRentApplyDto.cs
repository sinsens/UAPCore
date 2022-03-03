using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    /// <summary>
    /// 审核申请
    /// </summary>
    public class AuditRentApplyDto
    {
        /// <summary>
        /// 审核结果
        /// </summary>
        [DisplayName("审核结果")]
        [Required(ErrorMessage = "{0}是必填项")]
        public bool Result { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public IEnumerable<Guid> LockerIds { get; set; } = new List<Guid>();
    }
}
