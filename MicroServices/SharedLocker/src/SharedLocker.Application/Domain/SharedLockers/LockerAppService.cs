using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SharedLocker.Domain.SharedLockers.Dtos;
using SharedLocker.Permissions;
using SharedLocker.SharedLockers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using static SharedLocker.Permissions.SharedLockerPermissions;

namespace SharedLocker.Domain.SharedLockers
{
    [Authorize]
    public class LockerAppService : CrudAppService<Locker, LockerDto, Guid, PagedAndSortedLockerResultRequestDto, CreateUpdateLockerDto, CreateUpdateLockerDto>,
        ILockerAppService
    {

        private readonly LockerManager _lockerManager;
        private readonly IRepository<LockerRentInfo, Guid> _repositoryRentInfo;
        public LockerAppService(
            IRepository<Locker, Guid> repository,
            LockerManager lockerManager,
            IRepository<LockerRentInfo, Guid> repositoryRentInfo) : base(repository)
        {
            _lockerManager = lockerManager;
            _repositoryRentInfo = repositoryRentInfo;
        }

        /*
        public override async Task<LockerDto> GetAsync(Guid id)
        {
            var query = await Repository.GetQueryableAsync();
            var locker = await query
                .Include(x => x.RentInfos).ThenInclude(x => x.LockerRent)
                .FirstOrDefaultAsync(x => x.Id == id);

            return await MapToGetOutputDtoAsync(locker);
        }*/

        public async ValueTask<PagedResultDto<LockerRentInfoDto>> GetRentInfoListAsync(Guid id, PagedResultRequestDto input)
        {
            var query = await _repositoryRentInfo.GetQueryableAsync();

            query = query.Include(x => x.LockerRent).Where(x => x.LockerId == id);

            var count = await query.CountAsync();
            var rents = await query.Include(x => x.LockerRent)
                .OrderByDescending(x => x.Id)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<LockerRentInfoDto>()
            {
                TotalCount = count,
                Items = ObjectMapper.Map<IReadOnlyList<LockerRentInfo>, IReadOnlyList<LockerRentInfoDto>>(rents)
            };
        }

        public override async Task<LockerDto> UpdateAsync(Guid id, CreateUpdateLockerDto input)
        {
            var locker = await _lockerManager.UpdateAsync(id, input.Name, input.Number, input.Status, input.IsActive);
            return await MapToGetListOutputDtoAsync(locker);
        }

        public override async Task<PagedResultDto<LockerDto>> GetListAsync(PagedAndSortedLockerResultRequestDto input)
        {
            var query = await Repository.GetQueryableAsync();

            var likeFilter = $"%{input.Filter}%";

            query = query
                .WhereIf(input.AppId.HasValue, x => x.AppId == input.AppId)
                .WhereIf(input.Status.Any(), x => input.Status.Contains(x.Status))
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => EF.Functions.Like(x.Name, likeFilter) || EF.Functions.Like(x.Number.ToString(), likeFilter));

            var count = await query.CountAsync();
            var lockers = await query.OrderBy(x => x.Status).ThenByDescending(x => x.Id).PageBy(input).ToListAsync();

            return new PagedResultDto<LockerDto>
            {
                TotalCount = count,
                Items = ObjectMapper.Map<IReadOnlyList<Locker>, IReadOnlyList<LockerDto>>(lockers)
            };
        }

        public override async Task<LockerDto> CreateAsync(CreateUpdateLockerDto input)
        {
            var locker = await _lockerManager.CreateAsync(input.AppId, input.Name, input.Number, input.Status, true);

            return await MapToGetOutputDtoAsync(locker);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _lockerManager.DeleteAsync(id);
        }
    }
}
