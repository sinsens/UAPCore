using SharedLocker.Domain.SharedLockers.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace SharedLocker.Domain.SharedLockers
{
    internal interface IRentApplyAppService
    {
        /// <summary>
        /// 租用申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<LockerRentDto> RentApplyAsync(CreateLockerRentApplyDto input);

        /// <summary>
        /// 审核申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask AuditApplyAsync(AuditRentApplyDto input);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LockerRentDto>> GetListAsync(PagedAndSortedRentInfoResultCustomerRequestDto input);
    }
}
