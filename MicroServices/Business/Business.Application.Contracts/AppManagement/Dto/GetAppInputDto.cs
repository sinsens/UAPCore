using Volo.Abp.Application.Dtos;

namespace Business.AppManagement.Dto
{
    /// <summary>
    /// 分页查询
    /// </summary>
    public class GetAppInputDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 关键词
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// App类型
        /// </summary>
        public AppType? AppType { get; set; }
    }
}
