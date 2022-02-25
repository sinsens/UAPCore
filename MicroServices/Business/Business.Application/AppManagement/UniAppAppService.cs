using Business.AppManagement.Dto;
using Business.Domain.UniApp;
using Business.UniApp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Business.AppManagement
{
    public class UniAppAppService : CrudAppService<App, AppDto, Guid, GetAppInputDto, CreateOrUpdateAppDto>, IUniAppAppService
    {
        protected readonly AppManager AppManager;

        public UniAppAppService(AppManager appManager, IRepository<App, Guid> repository) : base(repository)
        {
            AppManager = appManager;
        }

        public override async Task<AppDto> CreateAsync(CreateOrUpdateAppDto input)
        {
            var app = await AppManager.CreateAsync(input.AppId, input.AppSecret, input.AppName, input.AppType, input.IsActive);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<App, AppDto>(app);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await AppManager.RemoveCacheAsync(id);
            await base.DeleteAsync(id);
        }

        public override async Task<AppDto> GetAsync(Guid id)
        {
            var app = await AppManager.GetCacheAsync(id);

            return await MapToGetOutputDtoAsync(app);
        }

        public override async Task<AppDto> UpdateAsync(Guid id, CreateOrUpdateAppDto input)
        {
            await AppManager.RemoveCacheAsync(id);
            return await base.UpdateAsync(id, input);
        }

        protected override async Task<IQueryable<App>> CreateFilteredQueryAsync(GetAppInputDto input)
        {
            var likeFilter = $"%{input.Filter}%";

            var query = await Repository.GetQueryableAsync();

            query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.AppId == input.Filter || EF.Functions.Like(x.AppName, likeFilter))
                .WhereIf(input.AppType.HasValue, x => x.AppType == input.AppType);

            return query;
        }
    }
}
