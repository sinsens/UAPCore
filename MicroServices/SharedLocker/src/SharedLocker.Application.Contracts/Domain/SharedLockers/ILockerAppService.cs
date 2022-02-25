using SharedLocker.Domain.SharedLockers.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SharedLocker.Domain.SharedLockers
{
    public interface ILockerAppService : IApplicationService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LockerDto> CreateAsync(CreateUpdateLockerDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LockerDto> GetAsync(Guid id);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LockerDto>> GetListAsync(PagedAndSortedLockerResultRequestDto input);

        /// <summary>
        /// 获取出租记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<PagedResultDto<LockerRentInfoDto>> GetRentInfoListAsync(Guid id, PagedResultRequestDto input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LockerDto> UpdateAsync(Guid id, CreateUpdateLockerDto input);
    }
}