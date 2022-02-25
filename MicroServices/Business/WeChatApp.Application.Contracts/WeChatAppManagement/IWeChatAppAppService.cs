using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WeChatApp.WeChatAppManagement.Dto;

namespace WeChatApp.WeChatAppManagement
{
    public interface IWeChatAppAppService : IApplicationService
    {
        Task<WeChatAppDto> Get(Guid id);

        Task<PagedResultDto<WeChatAppDto>> GetAll(GetWeChatAppInputDto input);

        Task<WeChatAppDto> DataPost(CreateOrUpdateWeChatAppDto input);

        Task Delete(List<Guid> ids);
    }
}
