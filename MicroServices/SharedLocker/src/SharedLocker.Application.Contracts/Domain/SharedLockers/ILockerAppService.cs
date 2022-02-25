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
        /// ���
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LockerDto> CreateAsync(CreateUpdateLockerDto input);

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LockerDto> GetAsync(Guid id);

        /// <summary>
        /// ��ҳ��ѯ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LockerDto>> GetListAsync(PagedAndSortedLockerResultRequestDto input);

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<PagedResultDto<LockerRentInfoDto>> GetRentInfoListAsync(Guid id, PagedResultRequestDto input);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LockerDto> UpdateAsync(Guid id, CreateUpdateLockerDto input);
    }
}