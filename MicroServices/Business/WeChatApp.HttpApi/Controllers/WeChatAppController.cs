using WeChatApp.WeChatAppManagement;
using WeChatApp.WeChatAppManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace WeChatApp.Controllers
{
    [RemoteService]
    [Area("WeChatApp")]
    [Route("api/app/WeChatApp")]
    public class WeChatAppController : AbpController
    {
        private readonly IWeChatAppAppService _WeChatAppAppService;

        public WeChatAppController(IWeChatAppAppService WeChatAppAppService)
        {
            _WeChatAppAppService = WeChatAppAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<WeChatAppDto> DataPost(CreateOrUpdateWeChatAppDto input)
        {
            return _WeChatAppAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _WeChatAppAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<WeChatAppDto> Get(Guid id)
        {
            return _WeChatAppAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<WeChatAppDto>> GetAll(GetWeChatAppInputDto input)
        {
            return _WeChatAppAppService.GetAll(input);
        }
    }
}
