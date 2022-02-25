using SharedLocker.Domain.SharedLockers.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SharedLocker.Domain.SharedLockers
{
    public interface ILockerRentAppService : IApplicationService
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask DiscardAsync(Guid id);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LockerRentDto> GetAsync(Guid id);

        /// <summary>
        /// 归还
        /// </summary>
        /// <returns></returns>
        ValueTask ReturnAsync(Guid rentId, ReturnLockerRentDto input);

        /// <summary>
        /// 租用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<LockerRentDto> RentAsync(CreateLockerRentDto input);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LockerRentDto>> GetListAsync(PagedAndSortedRentInfoResultRequestDto input);
    }
}