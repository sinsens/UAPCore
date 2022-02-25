using System;
using Volo.Abp.Application.Dtos;

namespace Business.AppUserManagement.Dto
{
    public class GetAppUserInputDto : PagedAndSortedResultRequestDto
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
        /// 性别
        /// </summary>
        public GenderEnum? Gender { get; set; }
    }
}
