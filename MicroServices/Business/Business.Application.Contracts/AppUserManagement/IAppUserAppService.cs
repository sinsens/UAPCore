using Business.AppUserManagement.Dto;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Business.AppUserManagement
{
    public interface IAppUserAppService : IApplicationService
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppUserDto> GetAsync(Guid id);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AppUserDto>> GetListAsync(GetAppUserInputDto input);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppUserDto> CreateAsync(CreateOrUpdateAppUserDto input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppUserDto> UpdateAsync(Guid id, CreateOrUpdateAppUserDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 更新手机号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask UpdatePhoneAsync(Guid id, UpdatePhoneRequestDto input);

        /// <summary>
        /// 创建微信账号用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<AppUserDto> CreateAsync(CreateWeiXinAppUserDto input);
    }
}
