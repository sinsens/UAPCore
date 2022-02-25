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
        /// ɾ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask DiscardAsync(Guid id);

        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LockerRentDto> GetAsync(Guid id);

        /// <summary>
        /// �黹
        /// </summary>
        /// <returns></returns>
        ValueTask ReturnAsync(Guid rentId, ReturnLockerRentDto input);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<LockerRentDto> RentAsync(CreateLockerRentDto input);

        /// <summary>
        /// ��ҳ��ѯ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LockerRentDto>> GetListAsync(PagedAndSortedRentInfoResultRequestDto input);
    }
}