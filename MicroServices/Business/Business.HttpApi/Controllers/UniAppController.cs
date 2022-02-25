using Business.AppManagement;
using Business.AppManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Business.Controllers
{
    [RemoteService]
    [Area("App")]
    [Route("api/app/uniApp")]
    public class UniAppController : AbpController, IUniAppAppService
    {
        private readonly IUniAppAppService _appAppService;
        public UniAppController(IUniAppAppService appAppService)
        {
            _appAppService = appAppService;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public Task<AppDto> CreateAsync(CreateOrUpdateAppDto input)
        {
            return _appAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _appAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public Task<AppDto> GetAsync(Guid id)
        {
            return _appAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getList")]
        public Task<PagedResultDto<AppDto>> GetListAsync(GetAppInputDto input)
        {
            return _appAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        public Task<AppDto> UpdateAsync(Guid id, CreateOrUpdateAppDto input)
        {
            return _appAppService.UpdateAsync(id, input);
        }
    }
}
