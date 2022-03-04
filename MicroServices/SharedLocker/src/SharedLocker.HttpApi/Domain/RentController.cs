using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLocker.Domain.SharedLockers;
using SharedLocker.Domain.SharedLockers.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace SharedLocker.Domain
{
    /// <summary>
    /// 储物柜出租管理
    /// </summary>
    [Authorize]
    [Route("/api/shared-locker/rent")]
    public class RentController : SharedLockerController, ILockerRentAppService
    {
        private readonly ILockerRentAppService _lockerRentAppService;
        public RentController(ILockerRentAppService lockerRentAppService)
        {
            _lockerRentAppService = lockerRentAppService;
        }

        /// <summary>
        /// 删除记录（作废后才能删除）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _lockerRentAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 作废记录（作废后才能删除）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("discard/{id}")]
        public ValueTask DiscardAsync(Guid id)
        {
            return _lockerRentAppService.DiscardAsync(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public Task<LockerRentDto> GetAsync(Guid id)
        {
            return _lockerRentAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getlist")]
        public Task<PagedResultDto<LockerRentDto>> GetListAsync(PagedAndSortedRentInfoResultRequestDto input)
        {
            return _lockerRentAppService.GetListAsync(input);
        }

        /// <summary>
        /// 租用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("rent")]
        public ValueTask<LockerRentDto> RentAsync(CreateLockerRentDto input)
        {
            return _lockerRentAppService.RentAsync(input);
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("return/{id}")]
        public ValueTask ReturnAsync(Guid id, ReturnLockerRentDto input)
        {
            return _lockerRentAppService.ReturnAsync(id, input);
        }
    }
}
