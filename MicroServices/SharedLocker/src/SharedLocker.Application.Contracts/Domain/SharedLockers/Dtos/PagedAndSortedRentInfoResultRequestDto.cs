﻿using SharedLocker.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    public class PagedAndSortedRentInfoResultRequestDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 关键词
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public Guid? AppId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public LockerRentStatus[] Status { get; set; }

        /// <summary>
        /// 租用开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 租用结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
