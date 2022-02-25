using SharedLocker.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    public class PagedAndSortedLockerResultRequestDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 关键词
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public LockerStatus[] Status { get; set; } = new LockerStatus[] {};

        /// <summary>
        /// 关联应用
        /// </summary>
        public Guid? AppId { get; set; }
    }
}
