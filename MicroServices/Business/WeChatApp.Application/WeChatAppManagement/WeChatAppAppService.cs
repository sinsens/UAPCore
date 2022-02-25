using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WeChatApp.WeChatAppManagement.Dto;
using WeChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using WeChatApp.Permissions;

namespace WeChatApp.WeChatAppManagement
{
    [Authorize(WeChatAppPermissions.WeChatApp.Default)]
    public class WeChatAppAppService : ApplicationService,IWeChatAppAppService
    {
        private IRepository<WeChatApp, Guid> _repository;

        public WeChatAppAppService(
            IRepository<WeChatApp, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<WeChatAppDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<WeChatApp, WeChatAppDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<WeChatAppDto>> GetAll(GetWeChatAppInputDto input)
        {
            var query = _repository.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<WeChatApp>, List<WeChatAppDto>>(items);
            return new PagedResultDto<WeChatAppDto>(totalCount, dto);
        }

        public async Task<WeChatAppDto> DataPost(CreateOrUpdateWeChatAppDto input)
        {
            WeChatApp result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateWeChatAppDto, WeChatApp>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<WeChatApp, WeChatAppDto>(result);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                await _repository.DeleteAsync(item);
            }

        }

     
        #endregion

    }
}