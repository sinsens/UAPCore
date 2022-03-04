using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLocker.Domain.SharedLockers;
using SharedLocker.Domain.SharedLockers.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace SharedLocker.Domain
{
    /// <summary>
    /// 储物柜管理
    /// </summary>
    [Authorize]
    [RemoteService]
    [Route("/api/shared-locker/locker")]
    public class LockerController: SharedLockerController, ILockerAppService
    {
        private readonly ILockerAppService _lockerAppService;
        public LockerController(ILockerAppService lockerAppService)
        {
            _lockerAppService = lockerAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public Task<LockerDto> CreateAsync(CreateUpdateLockerDto input)
        {
            return _lockerAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _lockerAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public Task<LockerDto> GetAsync(Guid id)
        {
            return _lockerAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getlist")]
        public Task<PagedResultDto<LockerDto>> GetListAsync(PagedAndSortedLockerResultRequestDto input)
        {
            return _lockerAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取出租记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getRentList/{id}")]
        public ValueTask<PagedResultDto<LockerRentInfoDto>> GetRentInfoListAsync(Guid id, PagedResultRequestDto input)
        {
            return _lockerAppService.GetRentInfoListAsync(id, input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public Task<LockerDto> UpdateAsync(Guid id, CreateUpdateLockerDto input)
        {
            return _lockerAppService.UpdateAsync(id, input);
        }
    }
}
