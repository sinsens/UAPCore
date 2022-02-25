using Business.AppUserManagement;
using Business.AppUserManagement.Dto;
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
    [Route("api/app/appUser")]
    internal class AppUserController:AbpController,IAppUserAppService
    {
        private readonly IAppUserAppService _appUserAppService;

        public AppUserController(IAppUserAppService appUserAppService)
        {
            _appUserAppService = appUserAppService;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public Task<AppUserDto> CreateAsync(CreateOrUpdateAppUserDto input)
        {
            return _appUserAppService.CreateAsync(input);
        }

        /// <summary>
        /// 创建微信 AppUser
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("createWxUser")]
        public ValueTask<AppUserDto> CreateAsync(CreateWeiXinAppUserDto input)
        {
            return _appUserAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _appUserAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public Task<AppUserDto> GetAsync(Guid id)
        {
            return _appUserAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getList")]
        public Task<PagedResultDto<AppUserDto>> GetListAsync(GetAppUserInputDto input)
        {
            return _appUserAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        public Task<AppUserDto> UpdateAsync(Guid id, CreateOrUpdateAppUserDto input)
        {
            return _appUserAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 更新手机号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updatePhone/{id}")]
        public ValueTask UpdatePhoneAsync(Guid id, UpdatePhoneRequestDto input)
        {
            return _appUserAppService.UpdatePhoneAsync(id, input);
        }
    }
}
