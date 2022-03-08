using SharedLocker.Domain.SharedLockers.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SharedLocker.Domain.SharedLockers
{
    public interface IRentApplyAppService : IApplicationService
    {
        /// <summary>
        /// 租用申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<LockerRentApplyDto> ApplyAsync(CreateLockerRentApplyDto input);


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<LockerRentApplyDto> GetAsync(Guid id);
		
		/// <summary>
        /// 获取最新审核中记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		ValueTask<LockerRentApplyDto> GetLastAsync();

        /// <summary>
        /// 审核申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask AuditAsync(Guid id, AuditRentApplyDto input);

        /// <summary>
        /// 作废申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask CancelAsync(Guid id, CancelApplyDto input);

        /// <summary>
        /// 作废申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask DiscardAsync(Guid id, DiscardApplyDto input);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<PagedResultDto<LockerRentApplyDto>> GetListAsync(PagedAndSortedRentApplyRequestDto input);

        /// <summary>
        /// 分页查询申请列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<PagedResultDto<LockerRentApplyDto>> GetMyListAsync(PagedAndSortedRentApplyCustomerRequestDto input);

        /// <summary>
        /// 分页查询服务中列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<PagedResultDto<LockerRentApplyDto>> GetProcessListAsync(PagedAndSortedRentApplyCustomerRequestDto input);
    }
}
